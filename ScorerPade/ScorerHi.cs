using MultiPrecision;
using MultiPrecisionAlgebra;
using MultiPrecisionCurveFitting;

namespace BarnesGPade {
    internal class ScorerHi {
        static void Main_() {

            List<(MultiPrecision<Pow2.N64> x, MultiPrecision<Pow2.N64> y)> expecteds = new();

            using StreamReader sr = new("../../../../results_disused/scorer_hi_n32.csv");

            sr.ReadLine();
            while (!sr.EndOfStream) {
                string? line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) {
                    break;
                }

                string[] line_split = line.Split(",");
                MultiPrecision<Pow2.N64> x = line_split[0], y = line_split[1];

                expecteds.Add((x, y));
            }

            List<(MultiPrecision<Pow2.N64> xmin, MultiPrecision<Pow2.N64> xmax, MultiPrecision<Pow2.N64> limit_range)> ranges = [
                (0, 1, 1d/4096), (1, 2, 1d/4096), (2, 4, 1d/4096), (4, 8, 1d/4096), (8, 16, 1d/4096), (16, 32, 1d/4096), (32, 64, 1d/4096)
            ];

            using StreamWriter sw = new("../../../../results_disused/scorer_hi_scaled_pade_table.csv");

            bool approximate(MultiPrecision<Pow2.N64> xmin, MultiPrecision<Pow2.N64> xmax) {
                Console.WriteLine($"[{xmin}, {xmax}]");

                List<(MultiPrecision<Pow2.N64> x, MultiPrecision<Pow2.N64> y)> expecteds_range =
                    expecteds.Where(item => item.x >= xmin && item.x <= xmax).ToList();

                Vector<Pow2.N64> xs = expecteds_range.Select(item => item.x).ToArray();
                Vector<Pow2.N64> ys = expecteds_range.Select(item => item.y).ToArray();

                if (xmin >= 2) {
                    ys *= xs;
                }

                xs -= xmin;

                SumTable<Pow2.N64> sum_table = new(xs, ys);

                for (int coefs = 5; coefs <= 72; coefs++) {
                    foreach ((int m, int n) in CurveFittingUtils.EnumeratePadeDegree(coefs, 1)) {
                        PadeFitter<Pow2.N64> pade = new(sum_table, m, n);

                        Vector<Pow2.N64> param = pade.Fit();
                        Vector<Pow2.N64> errs = pade.Error(param);

                        MultiPrecision<Pow2.N64> max_rateerr = CurveFittingUtils.MaxRelativeError(ys, pade.Regress(xs, param));

                        Console.WriteLine($"m={m},n={n}");
                        Console.WriteLine($"{max_rateerr:e20}");

                        if (max_rateerr > "1e-22") {
                            coefs += 4;
                            break;
                        }

                        if (max_rateerr < "1e-40") {
                            return false;
                        }

                        if (max_rateerr < "1e-31" &&
                            !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[..m], 0, xmax - xmin) &&
                            !CurveFittingUtils.HasLossDigitsPolynomialCoef(param[m..], 0, xmax - xmin)) {

                            sw.WriteLine($"x=[{xmin},{xmax}]");
                            sw.WriteLine($"samples={expecteds_range.Count}");
                            sw.WriteLine($"m={m},n={n}");
                            sw.WriteLine("numer");
                            foreach (var (_, val) in param[..m]) {
                                sw.WriteLine($"{val:e38}");
                            }
                            sw.WriteLine("denom");
                            foreach (var (_, val) in param[m..]) {
                                sw.WriteLine($"{val:e38}");
                            }

                            sw.WriteLine("coef");
                            foreach ((var numer, var denom) in CurveFittingUtils.EnumeratePadeCoef(param, m, n)) {
                                sw.WriteLine($"({ToFP128(numer)}, {ToFP128(denom)}),");
                            }

                            sw.WriteLine("relative err");
                            sw.WriteLine($"{max_rateerr:e20}");
                            sw.Flush();

                            return true;
                        }
                    }
                }

                return false;
            }

            Segmenter<Pow2.N64> segmenter = new(ranges, approximate);
            segmenter.Execute();

            foreach ((var xmin, var xmax, bool is_successs) in segmenter.ApproximatedRanges) {
                sw.WriteLine($"[{xmin},{xmax}],{(is_successs ? "OK" : "NG")}");
            }

            Console.WriteLine("END");
            Console.Read();
        }

        public static string ToFP128(MultiPrecision<Pow2.N64> x) {
            Sign sign = x.Sign;
            long exponent = x.Exponent;
            uint[] mantissa = x.Mantissa.Reverse().ToArray();

            string code = $"({(sign == Sign.Plus ? "+1" : "-1")}, {exponent}, 0x{mantissa[0]:X8}{mantissa[1]:X8}uL, 0x{mantissa[2]:X8}{mantissa[3]:X8}uL)";

            return code;
        }
    }
}
using MultiPrecision;

namespace ScorerPade {
    internal class ConvertLog2 {
        static void Main_() {
            using (StreamReader sr = new("../../../../results/scorer_hi_n32.csv")) {
                using StreamWriter sw = new("../../../../results_disused/scorer_hi_n32_log2.csv");

                sw.WriteLine(sr.ReadLine());

                while (!sr.EndOfStream) {
                    string? line = sr.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) {
                        break;
                    }

                    string[] line_split = line.Split(',');
                    string x = line_split[0];
                    MultiPrecision<Pow2.N32> y = line_split[1];

                    MultiPrecision<Pow2.N32> u = MultiPrecision<Pow2.N32>.Log2(y);

                    sw.WriteLine($"{x},{u}");
                    Console.WriteLine($"{x},{u:e20}");
                };
            }

            using (StreamReader sr = new("../../../../results/scorer_gi_n32.csv")) {
                using StreamWriter sw = new("../../../../results_disused/scorer_gi_n32_log2.csv");

                sw.WriteLine(sr.ReadLine());

                while (!sr.EndOfStream) {
                    string? line = sr.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) {
                        break;
                    }

                    string[] line_split = line.Split(',');
                    string x = line_split[0];

                    if (x[0] == '-') {
                        continue;
                    }

                    MultiPrecision<Pow2.N32> y = line_split[1];

                    MultiPrecision<Pow2.N32> u = MultiPrecision<Pow2.N32>.Log2(y);

                    sw.WriteLine($"{x},{u}");
                    Console.WriteLine($"{x},{u:e20}");
                };
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
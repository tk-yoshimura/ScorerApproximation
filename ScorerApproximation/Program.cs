using MultiPrecision;

namespace ScorerApproximation {
    internal class Program {
        static void Main() {
            using (StreamWriter sw = new("../../../../results_disused/scorer_hi_n32.csv")) {
                sw.WriteLine("x,scorer_hi(x)");

                for (double h = 1; h <= 32; h *= 2) {
                    for (double x = h > 1 ? h / 2 : 0; x < 2 * h; x += h / 8192) { 
                        MultiPrecision<Pow2.N32> y = ScorerN32.Hi(-x);

                        sw.WriteLine($"{x},{y}");
                        Console.WriteLine($"{x},{y:e40}");
                    }
                }
                {
                    MultiPrecision<Pow2.N32> y = ScorerN32.Hi(-64);

                    sw.WriteLine($"{64},{y}");
                    Console.WriteLine($"{64},{y:e40}");
                }

                sw.Close();
            }

            using (StreamWriter sw = new("../../../../results_disused/scorer_gi_n32.csv")) {
                sw.WriteLine("x,scorer_gi(x)");
                
                for (double h = 1; h <= 32; h *= 2) {
                    for (double x = h > 1 ? h / 2 : 0; x < 2 * h; x += h / 8192) { 
                        MultiPrecision<Pow2.N32> y = ScorerN32.Gi(x);

                        sw.WriteLine($"{x},{y}");
                        Console.WriteLine($"{x},{y:e40}");
                    }
                }
                {
                    MultiPrecision<Pow2.N32> y = ScorerN32.Gi(64);

                    sw.WriteLine($"{64},{y}");
                    Console.WriteLine($"{64},{y:e40}");
                }

                sw.Close();
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
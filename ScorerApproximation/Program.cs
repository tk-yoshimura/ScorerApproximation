using MultiPrecision;

namespace ScorerApproximation {
    internal class Program {
        static void Main() {
            using (StreamWriter sw = new("../../../../results/scorer_hi.csv")) {
                sw.WriteLine("x,scorer_hi(x)");

                for (double x = -256; x <= 128; x += 1 / 256d) {
                    MultiPrecision<Pow2.N32> y = ScorerN32.Hi(x);

                    sw.WriteLine($"{x},{y}");
                    Console.WriteLine($"{x},{y:e40}");
                }

                sw.Close();
            }

            using (StreamWriter sw = new("../../../../results/scorer_gi.csv")) {
                sw.WriteLine("x,scorer_gi(x)");
                
                for (double x = -32; x <= 256; x += 1 / 256d) {
                    MultiPrecision<Pow2.N32> y = ScorerN32.Gi(x);

                    sw.WriteLine($"{x},{y}");
                    Console.WriteLine($"{x},{y:e40}");
                }

                sw.Close();
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
using MultiPrecision;

namespace ScorerApproximation {
    internal class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N32> y = ScorerGi<Pow2.N32>.Asymptotic(1000);

            Console.WriteLine(y);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
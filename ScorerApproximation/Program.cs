using MultiPrecision;

namespace ScorerApproximation {
    internal class Program {
        static void Main() {
            double x = -112.5;
            MultiPrecision<Pow2.N32> ydec = ScorerN32.Hi(MultiPrecision<Pow2.N32>.BitDecrement(x));
            MultiPrecision<Pow2.N32> y = ScorerN32.Hi(x);
            MultiPrecision<Pow2.N32> yinc = ScorerN32.Hi(MultiPrecision<Pow2.N32>.BitIncrement(x));

            Console.WriteLine(ydec);
            Console.WriteLine(y);
            Console.WriteLine(yinc);

            //using (StreamWriter sw = new("../../../../results/scorer_hi.csv")) {
            //    sw.WriteLine("x,scorer_hi(x)");

            //    for (double x = -256; x <= 256; x += 0.25) {
            //        MultiPrecision<Pow2.N32> y = ScorerN32.Hi(x);

            //        sw.WriteLine($"{x},{y}");
            //        Console.WriteLine($"{x},{y:e40}");
            //    }

            //    sw.Close();
            //}

            //using (StreamWriter sw = new("../../../../results/scorer_gi.csv")) {
            //    sw.WriteLine("x,scorer_gi(x)");
                
            //    for (double x = -32; x <= 256; x += 0.25) {
            //        MultiPrecision<Pow2.N32> y = ScorerN32.Gi(x);

            //        sw.WriteLine($"{x},{y}");
            //        Console.WriteLine($"{x},{y:e40}");
            //    }

            //    sw.Close();
            //}

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
using MultiPrecision;

namespace ScorerApproximation {
    public static class MaclaurinSeries<N> where N : struct, IConstant {
        private static readonly List<MultiPrecision<N>> table = new() {
            1,
            MultiPrecision<N>.Cbrt(3),
            MultiPrecision<N>.Square(MultiPrecision<N>.Cbrt(3)) / 2
        };

        public static MultiPrecision<N> Value(int n) {
            if (n >= table.Count) {
                for (int k = table.Count; k <= n; k++) {
                    MultiPrecision<N> c = table[k - 3] * MultiPrecision<N>.Div(3, checked((long)k * (k - 1) * (k - 2)));
                    table.Add(c);
                }
            }

            return table[n];
        }
    }
}

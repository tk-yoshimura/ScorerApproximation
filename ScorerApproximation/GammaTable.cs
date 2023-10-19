using MultiPrecision;

namespace ScorerApproximation {
    public static class GammaTable<N> where N : struct, IConstant {
        private static readonly List<MultiPrecision<N>> table = new() {
            MultiPrecision<N>.NaN,
            MultiPrecision<N>.Gamma(MultiPrecision<N>.Div(1, 3)),
            MultiPrecision<N>.Gamma(MultiPrecision<N>.Div(2, 3)),
            1,
            MultiPrecision<N>.Gamma(MultiPrecision<N>.Div(4, 3)),
            MultiPrecision<N>.Gamma(MultiPrecision<N>.Div(5, 3)),
            1
        };

        public static MultiPrecision<N> Value(int n) {
            if (n >= table.Count) {
                for (int k = table.Count; k <= n; k++) {
                    MultiPrecision<N> c = table[k - 3] * MultiPrecision<N>.Div(k - 3, 3);
                    table.Add(c);
                }
            }

            return table[n];
        }
    }
}

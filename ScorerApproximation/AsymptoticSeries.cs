using MultiPrecision;

namespace ScorerApproximation {
    public static class AsymptoticSeries<N> where N : struct, IConstant {
        private static readonly List<MultiPrecision<N>> table = new() {
            1,
        };

        public static MultiPrecision<N> Value(int n) {
            if (n >= table.Count) {
                for (int k = table.Count; k <= n; k++) {
                    MultiPrecision<N> c = table[k - 1] * MultiPrecision<N>.Div(checked(3L * k * (3L * k - 1) * (3L * k - 2)), 3 * k);
                    table.Add(c);
                }
            }

            return table[n];
        }
    }
}

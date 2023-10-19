using MultiPrecision;

namespace ScorerApproximation {
    public static class ScorerHi<N> where N : struct, IConstant {
        public static readonly MultiPrecision<N> C = 1 / (MultiPrecision<N>.Square(MultiPrecision<N>.Cbrt(3)) * MultiPrecision<N>.PI);

        public static MultiPrecision<N> NearZero(MultiPrecision<N> x, int max_terms = 1024) {
            MultiPrecision<N> x2 = x * x;
            MultiPrecision<N> s = 0, u = C;

            for (int k = 0; k <= max_terms; k += 2) {
                MultiPrecision<N> ds = u * (NearZeroCoef(k) + x * NearZeroCoef(k + 1));
                s += ds;

                if (ds.Exponent <= s.Exponent - MultiPrecision<N>.Bits) {
                    return s;
                }

                u *= x2;
            }

            return MultiPrecision<N>.NaN;
        }

        public static MultiPrecision<N> Asymptotic(MultiPrecision<N> x, int max_terms = 1024) {
            if (MultiPrecision<N>.IsPositive(x)) {
                return MultiPrecision<N>.NaN;
            }

            MultiPrecision<N> v3 = 1 / (x * x * x), v6 = v3 * v3;
            MultiPrecision<N> s = 0, u = -1 / (MultiPrecision<N>.PI * x);

            for (int k = 0; k <= max_terms; k += 2) {
                MultiPrecision<N> ds = u * (AsymptoticSeries<N>.Value(k) + v3 * AsymptoticSeries<N>.Value(k + 1));
                s += ds;

                if (ds.Exponent <= s.Exponent - MultiPrecision<N>.Bits) {
                    return s;
                }

                if (ds.Sign != Sign.Plus) {
                    break;
                }

                u *= v6;
            }

            return MultiPrecision<N>.NaN;
        }

        private static readonly List<MultiPrecision<N>> nearzero_coefs = new();
        public static MultiPrecision<N> NearZeroCoef(int n) {
            if (n >= nearzero_coefs.Count) {
                for (int k = nearzero_coefs.Count; k <= n; k++) {
                    MultiPrecision<N> c = GammaTable<N>.Value(k + 1) * MaclaurinSeries<N>.Value(k);
                    nearzero_coefs.Add(c);
                }
            }

            return nearzero_coefs[n];
        }
    }
}

using BarnesGApproximation;
using MultiPrecision;

namespace ScorerApproximation {
    public static class ScorerN32 {
        public static MultiPrecision<Pow2.N32> Hi(MultiPrecision<Pow2.N32> x) {
            if (x < -112.5) {
                return ScorerHi<Plus2<Pow2.N32>>.Asymptotic(x.Convert<Plus2<Pow2.N32>>()).Convert<Pow2.N32>();
            }
            else {
                return ScorerHi<Plus40<Pow2.N32>>.NearZero(x.Convert<Plus40<Pow2.N32>>(), 8192).Convert<Pow2.N32>();
            }
        }

        public static MultiPrecision<Pow2.N32> Gi(MultiPrecision<Pow2.N32> x) {
            if (x >= 113) {
                return ScorerGi<Plus2<Pow2.N32>>.Asymptotic(x.Convert<Plus2<Pow2.N32>>()).Convert<Pow2.N32>();
            }
            else {
                return ScorerGi<Plus40<Pow2.N32>>.NearZero(x.Convert<Plus40<Pow2.N32>>(), 8192).Convert<Pow2.N32>();
            }
        }
    }
}

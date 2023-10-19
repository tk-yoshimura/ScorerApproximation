using BarnesGApproximation;
using MultiPrecision;

namespace ScorerApproximation {
    public static class ScorerN32 {
        public static MultiPrecision<Pow2.N32> Hi(MultiPrecision<Pow2.N32> x) {
            if (x < -112.5) {
                return ScorerHi<Plus4<Pow2.N32>>.Asymptotic(x.Convert<Plus4<Pow2.N32>>()).Convert<Pow2.N32>();
            }
            else {
                return ScorerHi<Plus4<Pow2.N32>>.NearZero(x.Convert<Plus4<Pow2.N32>>(), 8192).Convert<Pow2.N32>();
            }
        }

        public static MultiPrecision<Pow2.N32> Gi(MultiPrecision<Pow2.N32> x) {
            if (x >= 113) {
                return ScorerGi<Plus4<Pow2.N32>>.Asymptotic(x.Convert<Plus4<Pow2.N32>>()).Convert<Pow2.N32>();
            }
            else if (x >= -16) {
                return ScorerGi<Plus4<Pow2.N32>>.NearZero(x.Convert<Plus4<Pow2.N32>>(), 8192).Convert<Pow2.N32>();
            }
            else if (x >= -32) {
                return ScorerGi<Plus8<Pow2.N32>>.NearZero(x.Convert<Plus8<Pow2.N32>>(), 4096).Convert<Pow2.N32>();
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}

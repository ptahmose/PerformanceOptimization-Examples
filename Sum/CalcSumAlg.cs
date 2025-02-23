namespace Sum
{
    internal class CalcSumAlg : ICalcSum
    {
        public ulong CalcSum(ulong n)
        {
            return n * (n + 1) / 2;
        }
    }
}

namespace Sum
{
    using System.Diagnostics;
    using System.Numerics;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Vector<ulong>.Count = {Vector<ulong>.Count}");

            const int repeatCount = 2;
            const ulong n = 4_000_000_000;

            ICalcSum calcSum = new NaiveCalcSum();
            ulong sum = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine($"NaiveCalcSum: {sum} time: {stopwatch.Elapsed.TotalSeconds:F2}s");

            calcSum = new SimdCalcSum();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine($"SIMDCalcSum: {sum} time: {stopwatch.Elapsed.TotalSeconds:F2}s");

            calcSum = new CalcSumAlg();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine($"AlgCalcSum: {sum} time: {stopwatch.Elapsed.TotalSeconds:F2}s");

            calcSum = new ParallelCalcSum();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine($"ParallelCalcSum: {sum} time: {stopwatch.Elapsed.TotalSeconds:F2}s");

        }
    }
}

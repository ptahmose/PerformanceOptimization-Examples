using System.Diagnostics;

namespace Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int repeatCount = 3;
            const ulong n = 3_000_000_000;

            ICalcSum calcSum = new NaiveCalcSum();
            ulong sum = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine("NaiveCalcSum: {0} time: {1}s", sum, stopwatch.Elapsed.Seconds);

            calcSum = new SimdCalcSum();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repeatCount; i++)
            {
                sum = calcSum.CalcSum(n);
            }

            stopwatch.Stop();
            Console.WriteLine("NaiveCalcSum: {0} time: {1}s", sum, stopwatch.Elapsed.Seconds);
        }
    }
}

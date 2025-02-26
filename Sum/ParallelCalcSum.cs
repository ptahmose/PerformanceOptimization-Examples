namespace Sum
{
    using System;
    using System.Threading.Tasks;

    internal class ParallelCalcSum : ICalcSum
    {
        public ulong CalcSum(ulong n)
        {
            const ulong MinNumberForPartition = 100_000_000;

            int numProcs = Environment.ProcessorCount;
            int numPartitions = (int)Math.Min(n / MinNumberForPartition, (ulong)numProcs);

            ulong partitionSize = n / (ulong)numPartitions;

            ulong totalSum = 0;

            // Use Parallel.For to process each partition concurrently.
            // The loop iterates over the partition index (from 0 to numPartitions - 1).
            Parallel.For(
                0,
                numPartitions,
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                partition =>
                {
                    // Calculate the start index for this partition.
                    ulong start = (ulong)partition * partitionSize;

                    // For the last partition, include any remainder.
                    ulong end;
                    if (partition == numPartitions - 1)
                    {
                        end = n + 1;
                    }
                    else
                    {
                        end = start + partitionSize;
                    }

                    // Compute the partial sum for this partition.
                    ulong sum = 0;
                    for (ulong i = start; i < end; i++)
                    {
                        sum += i;
                    }

                    // Aggregate the result.
                    Interlocked.Add(ref totalSum, sum);
                }
            );

            return totalSum;
        }
    }
}

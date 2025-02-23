namespace Sum
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Metrics;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    internal class SimdCalcSum : ICalcSum
    {
        public ulong CalcSum(ulong n)
        {
            // Determine how many 'ulong' values fit in a SIMD vector.
            // This tells us how many numbers we can process in parallel.
            int vectorSize = Vector<ulong>.Count;

            // Create an array to initialize our SIMD vector with a sequence of numbers.
            // For example, if vectorSize is 4, the array will contain [0, 1, 2, 3].
            ulong[] initializationValues = new ulong[vectorSize];
            for (int i = 0; i < vectorSize; i++)
            {
                initializationValues[i] = (ulong)i;
            }

            // Initialize a SIMD vector with the sequence [0, 1, 2, ... vectorSize-1].
            // This vector, 'value', represents the starting block of numbers to add.
            Vector<ulong> value = new Vector<ulong>(initializationValues);

            // Create a SIMD vector where every element is 'vectorSize'.
            // This is used to increment 'value' in each iteration, so the next block becomes:
            // [vectorSize, vectorSize+1, ..., 2*vectorSize-1], and so on.
            Vector<ulong> summand = new Vector<ulong>((ulong)vectorSize);

            // Calculate the total number of elements to sum.
            // Since we are summing numbers from 0 to n (inclusive), totalCount is n+1.
            ulong totalCount = n + 1;

            // Compute the upper bound (limit) for our SIMD loop.
            // This is the largest multiple of vectorSize that does not exceed totalCount.
            // It ensures that our vectorized loop only processes complete blocks.
            ulong limit = totalCount - (totalCount % (ulong)vectorSize);

            // Initialize a SIMD vector to accumulate the partial sums.
            Vector<ulong> vectorSum = Vector<ulong>.Zero;

            // 'count' will track our current position (number) in the overall sequence.
            ulong count = 0;

            // Process numbers in blocks of 'vectorSize' using a vectorized loop.
            // In each iteration:
            //   - 'value' contains a block of consecutive numbers starting at 'count'
            //   - We add these numbers to 'vectorSum' (accumulating the partial sum)
            //   - We then update 'value' by adding 'summand' so that it holds the next block of numbers.
            for (; count < limit; count += (ulong)vectorSize)
            {
                // Add the current block of numbers to the running SIMD sum.
                vectorSum = Vector.Add(vectorSum, value);
                // Increment the block: for each element in 'value', add vectorSize.
                // This moves the block to the next set of consecutive numbers.
                value = Vector.Add(value, summand);
            }

            // At this point, the SIMD loop has processed all complete blocks.
            // Now we need to combine the elements of the SIMD vector 'vectorSum' into a single scalar sum.
            ulong sum = 0;
            for (int j = 0; j < vectorSize; j++)
            {
                sum += vectorSum[j];
            }

            // Process any remaining numbers that did not form a complete block.
            // This loop handles the "tail" of the sequence if totalCount is not a multiple of vectorSize.
            for (; count < totalCount; count++)
            {
                sum += count;
            }

            // Return the final computed sum.
            return sum;
        }
    }
}

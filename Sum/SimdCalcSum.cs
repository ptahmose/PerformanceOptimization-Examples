using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sum
{
    internal class SimdCalcSum : ICalcSum
    {
        public ulong CalcSum(ulong n)
        {
            // Determine the SIMD vector size for ulong.
            int vectorSize = Vector<ulong>.Count;

            ulong[] initalizationValues = new ulong[vectorSize];
            for (int i = 0; i < vectorSize; i++)
            {
                initalizationValues[i] = (ulong)i;
            }

            Vector<ulong> value = new Vector<ulong>(initalizationValues);
            Vector<ulong> summand = new Vector<ulong>((ulong)vectorSize);//new ulong[] { 4, 4, 4, 4 });

            // Total count of numbers from 0 to n inclusive.
            ulong totalCount = n + 1;
            // Compute the upper bound for the vectorized loop.
            ulong limit = totalCount - (totalCount % (ulong)vectorSize);

            Vector<ulong> vectorSum = Vector<ulong>.Zero;
            ulong count = 0;
            for (; count < limit; count += (ulong)vectorSize)
            {
                vectorSum = Vector.Add(vectorSum, value);
                value = Vector.Add(value, summand);
            }

            // Reduce the vector sum into a scalar.
            ulong sum = 0;
            for (int j = 0; j < vectorSize; j++)
            {
                sum += vectorSum[j];
            }

            // Process any remaining elements.
            for (; count < totalCount; count++)
            {
                sum += count;
            }

            /*
            // Determine the SIMD vector size for ulong.
            int vectorSize = Vector<ulong>.Count;
            Vector<ulong> vectorSum = Vector<ulong>.Zero;

            // Precompute an offset vector: [0, 1, 2, ..., vectorSize-1].
            ulong[] offsetsArray = new ulong[vectorSize];
            for (int i = 0; i < vectorSize; i++)
            {
                offsetsArray[i] = (ulong)i;
            }

            Vector<ulong> offsets = new Vector<ulong>(offsetsArray);

            // Total count of numbers from 0 to n inclusive.
            ulong totalCount = n + 1;
            // Compute the upper bound for the vectorized loop.
            ulong limit = totalCount - (totalCount % (ulong)vectorSize);

            ulong count = 0;
            // Process numbers in chunks of vectorSize.
            for (; count < limit; count += (ulong)vectorSize)
            {
                // Create a base vector where every element is 'i'.
                Vector<ulong> baseVector = new Vector<ulong>(count);
                // Add the constant offsets to generate [i, i+1, ..., i+vectorSize-1].
                Vector<ulong> values = baseVector + offsets;
                // Accumulate the result.
                vectorSum += values;
            }

            // Reduce the vector sum into a scalar.
            ulong sum = 0;
            for (int j = 0; j < vectorSize; j++)
            {
                sum += vectorSum[j];
            }

            // Process any remaining elements.
            for (; count < totalCount; count++)
            {
                sum += count;
            }*/

            return sum;
        }
    }
}

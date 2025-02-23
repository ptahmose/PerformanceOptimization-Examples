﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sum
{
    internal class NaiveCalcSum : ICalcSum
    {
        public ulong CalcSum(ulong n)
        {
            ulong sum = 0;
            for (ulong i = 0; i <= n; i++)
            {
                sum += i;
            }

            return sum;
        }
    }
}

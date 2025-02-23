using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

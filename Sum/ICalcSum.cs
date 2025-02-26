namespace Sum
{
    internal interface ICalcSum
    {
        /// <summary>   
        /// Calculates the sum of all integers from zero up to (and including) n. 
        /// </summary>
        /// <param name="n">The highest integer to include in the sum. </param>
        /// <returns>   The calculated sum. </returns>
        ulong CalcSum(ulong n);
    }
}

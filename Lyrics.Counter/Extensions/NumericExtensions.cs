using System;
using System.Collections.Generic;
using System.Linq;

namespace Music.Lyrics.Word.Counter.Extensions
{
    public static class NumericExtensions
    {
        public static double Truncate (this double d, byte decimals)
        {
            double r = Math.Round (d, decimals);

            if (d > 0 && r > d)
            {
                return r - 0.01;
            }
            else if (d < 0 && r < d)
            {
                return r + 0.01;
            }

            return r;
        }

        public static double StandardDeviation (this IEnumerable<int> values)
        {
            double mean = values.Sum () / values.Count ();

            var squaresQuery = from int value in values
            select (value - mean) * (value - mean);

            double sumOfSquares = squaresQuery.Sum ();

            return Math.Sqrt (sumOfSquares / values.Count ());
        }
    }
}
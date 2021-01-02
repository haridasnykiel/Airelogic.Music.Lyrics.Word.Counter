using System;

namespace Music.Lyrics.Word.Counter.Extensions
{
    public static class DoubleExtensions
    {
        public static double Truncate(this double d, byte decimals)
        {
            double r = Math.Round(d, decimals);

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
    }
}
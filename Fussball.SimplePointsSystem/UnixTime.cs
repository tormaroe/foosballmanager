using System;

namespace Fussball.SimplePointsSystem
{
    internal static class UnixTime
    {
        private static readonly DateTime EPOC = new DateTime(1970, 1, 1, 0, 0, 0);

        internal static double GetUnixTime(DateTime d)
        {
            TimeSpan span = d - EPOC;
            return span.TotalSeconds;
        }

        internal static DateTime GetDateFromUnixTime(double u)
        {
            return EPOC.AddSeconds(u);
        }
    }
}

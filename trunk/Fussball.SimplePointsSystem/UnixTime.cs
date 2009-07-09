using System;

namespace Fussball.SimplePointsSystem
{
    internal static class UnixTime
    {
        internal static double GetUnixTime(DateTime d)
        {
            TimeSpan span = d - new DateTime(1970, 1, 1, 0, 0, 0);
            return span.TotalSeconds;
        }

        internal static DateTime GetDateFromUnixTime(double u)
        {
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0);
            return d.AddSeconds(u);
        }
    }
}

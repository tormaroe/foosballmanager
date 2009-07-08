using System;
using System.Collections.Generic;
using System.Text;

namespace Fussball.SimplePointsSystem
{
    internal class Common
    {
        internal static string CreateElement(string elementName, string innerText)
        {
            return string.Format("<{0}>{1}</{0}>", elementName, innerText);
        }

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

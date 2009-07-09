using System;
using System.Collections.Generic;
using System.Text;

namespace Fussball.SimplePointsSystem
{
    internal static class CommonXml
    {
        internal static string CreateElement(string elementName, string innerText)
        {
            return string.Format("<{0}>{1}</{0}>", elementName, innerText);
        }
    }
}
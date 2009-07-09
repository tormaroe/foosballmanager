using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fussball.SimplePointsSystem
{
    public class AuditTrailItem
    {
        public DateTime When { get; internal set; }
        public string What { get; internal set; }
        public string CssAttributes { get; internal set; }

        public bool IsRegisteredAtTheSameTimeAs(AuditTrailItem anotherItem)
        {
            return When.Year.Equals(anotherItem.When.Year)
                    && When.DayOfYear.Equals(anotherItem.When.DayOfYear)
                    && When.Hour.Equals(anotherItem.When.Hour)
                    && When.Minute.Equals(anotherItem.When.Minute)
                    && When.Second.Equals(anotherItem.When.Second);
        }
    }
}

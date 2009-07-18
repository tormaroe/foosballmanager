using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    public class GameResultSpecificationsContext
    {
        protected const int five_points = 5;
        protected const int ten_points = 10;
        protected const int twenty_points = 20;

        [SetUp]
        public void SetUp()
        {
            AuditTrail.Instance = new AuditTrail();
        }
    }
}

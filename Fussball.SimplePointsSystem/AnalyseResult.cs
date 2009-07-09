using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fussball.SimplePointsSystem
{
    public class AnalyseResult
    {
        public int GamesWonByPlayer1 { get; set; }
        public int GamesWonByPlayer2 { get; set; }
        public int PointsEarnedByPlayer1 { get; set; }
        public int PointsEarnedByPlayer2 { get; set; }
        public int PointsLostByPlayer1 { get; set; }
        public int PointsLostByPlayer2 { get; set; }
    }
}

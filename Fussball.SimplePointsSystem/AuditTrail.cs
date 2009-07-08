using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fussball.SimplePointsSystem
{
    public class AuditTrail
    {
        public static AuditTrail Instance
        {
            get
            {                
                return System.Web.HttpContext.Current.Application["AuditTrail"] as AuditTrail;
            }
            set
            {
                System.Web.HttpContext.Current.Application["AuditTrail"] = value;
            }
        }

        private List<AuditTrailItem> _items;

        public DataView DefaultView
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("When", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("What", typeof(string)));
                dt.Columns.Add(new DataColumn("Css", typeof(string)));

                DataRow dr;
                foreach (AuditTrailItem item in _items)
                {
                    dr = dt.NewRow();
                    dr[0] = item.When;
                    dr[1] = item.What;
                    dr[2] = item.CssAttributes;

                    dt.Rows.Add(dr);
                }

                DataView dv = dt.DefaultView;
                dv.Sort = "When DESC";
                return dv;
            }
        }

        public AuditTrail()
        {
            _items = new List<AuditTrailItem>();
        }

        public AuditTrail(string xml)
        {
            _items = new List<AuditTrailItem>();

            //throw new Exception(xml);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList itemNodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode node in itemNodes)
            {
                AuditTrailItem item = new AuditTrailItem();
                item.When = Common.GetDateFromUnixTime(Convert.ToDouble(node.SelectSingleNode("when").InnerText));
                item.What = node.SelectSingleNode("what").InnerText;
                item.CssAttributes = node.SelectSingleNode("css").InnerText;

                _items.Add(item);
            }
        }

        public string ToXml()
        {
            StringBuilder xml = new StringBuilder("<audittrail>");

            foreach (AuditTrailItem item in _items)
            {
                xml.AppendFormat("<item><when>{0}</when><what>{1}</what><css>{2}</css></item>",
                    Common.GetUnixTime(item.When),
                    item.What,
                    item.CssAttributes);
            }

            xml.Append("</audittrail>");
            return xml.ToString();
        }

        public void AddManualAudit(string message)
        {
            AuditTrailItem item = new AuditTrailItem();
            item.When = DateTime.Now;
            item.What = message;
            item.CssAttributes = "color:black;background-color:silver;";

            _items.Add(item);
        }

        public void AddSinglesMatch(Player winner, Player looser)
        {
            AuditTrailItem item = new AuditTrailItem();
            item.When = DateTime.Now;
            item.What = string.Format("{0} won a singles match agains {1}.",
                winner.Name,
                looser.Name);
            item.CssAttributes = "color:white;background-color:green;";

            _items.Add(item);
        }

        public void AddDoublesMatch(Player winner1, Player winner2, Player looser1, Player looser2)
        {
            AuditTrailItem item = new AuditTrailItem();
            item.When = DateTime.Now;
            item.What = string.Format("{0} and {1} won a doubles match agains {2} and {3}.",
                winner1.Name, winner2.Name,
                looser1.Name, looser2.Name);
            item.CssAttributes = "color:white;background-color:blue;";

            _items.Add(item);
        }

        public void AddPointsChange(Player player, int pointsAdded, int newPoints)
        {
            AuditTrailItem item = new AuditTrailItem();
            item.When = DateTime.Now;
            item.What = string.Format("{0}'s score changed to {1} ({2} points was {3}).",
                player.Name, 
                newPoints,
                Math.Abs(pointsAdded),
                (pointsAdded >= 0 ? "added" : "removed"));
            item.CssAttributes = "color:white;background-color:black;";

            _items.Add(item);
        }

        public AnalyseResult AnalyseMatches(string playerName1, string playerName2)
        {
            AnalyseResult result = new AnalyseResult();

            string player1WonText = string.Format("{0} won a singles match agains {1}.",
                playerName1,
                playerName2);
            string player2WonText = string.Format("{0} won a singles match agains {1}.",
                playerName2,
                playerName1);

            foreach (AuditTrailItem item in _items)
            {
                if (item.What.Equals(player1WonText))
                {
                    result.GamesWonByPlayer1++;
                    GetPointsWonAndLost(result, item, playerName1, playerName2, true);
                }
                else if (item.What.Equals(player2WonText))
                {
                    result.GamesWinByPlayer2++;
                    GetPointsWonAndLost(result, item, playerName2, playerName1, false);
                }
            }

            return result;
        }

        public AnalysePlayerResult AnalysePlayer(string playerName)
        {
            string playerScoreChangedText = string.Format("{0}'s score changed to ",
                playerName);

            AnalysePlayerResult result = new AnalysePlayerResult();
            result.Points.Add(Constants.DEFAULT_PLAYER_POINTS);

            foreach (AuditTrailItem item in _items)
            {
                if (item.What.StartsWith(playerScoreChangedText))
                {
                    string sPoints = item.What.Substring(playerScoreChangedText.Length);
                    sPoints = sPoints.Substring(0, sPoints.IndexOf(" "));
                    result.Points.Add(Int32.Parse(sPoints));
                }
            }

            return result;
        }

        private void GetPointsWonAndLost(AnalyseResult result, AuditTrailItem matchItem, string winnerName, string looserName, bool player1WasTheWinner)
        {
            string winnerWhatPrefix = string.Format("{0}'s score changed to", winnerName);
            string looserWhatPrefix = string.Format("{0}'s score changed to", looserName);

            foreach (AuditTrailItem item in _items)
            {
                if (matchItem.When.Year.Equals(item.When.Year)
                    && matchItem.When.DayOfYear.Equals(item.When.DayOfYear)
                    && matchItem.When.Hour.Equals(item.When.Hour)
                    && matchItem.When.Minute.Equals(item.When.Minute)
                    && matchItem.When.Second.Equals(item.When.Second))
                {
                    if (!matchItem.What.Equals(item.What))
                    {
                        int changeStartIndex = item.What.IndexOf("(") + 1;
                        int changeEndIndex = item.What.IndexOf(" points was") -1;

                        if (item.What.StartsWith(winnerWhatPrefix))
                        {
                            if (player1WasTheWinner)
                            {
                                result.PointsEarnedByPlayer1 += Int32.Parse(item.What.Substring(changeStartIndex, changeEndIndex-changeStartIndex+1));
                            }
                            else
                            {
                                result.PointsEarnedByPlayer2 += Int32.Parse(item.What.Substring(changeStartIndex, changeEndIndex - changeStartIndex + 1));
                            }
                        }
                        else if (item.What.StartsWith(looserWhatPrefix))
                        {
                            if (player1WasTheWinner)
                            {
                                result.PointsLostByPlayer2 += Int32.Parse(item.What.Substring(changeStartIndex, changeEndIndex - changeStartIndex + 1));
                            }
                            else
                            {
                                result.PointsLostByPlayer1 += Int32.Parse(item.What.Substring(changeStartIndex, changeEndIndex - changeStartIndex + 1));
                            }
                        }
                    }
                }
            }
        }

        public class AnalyseResult
        {
            private int _numWonByPlayer1;

            public int GamesWonByPlayer1
            {
                get
                {
                    return _numWonByPlayer1;
                }
                set
                {
                    _numWonByPlayer1 = value;
                }
            }

            private int _numWonByPlayer2;

            public int GamesWinByPlayer2
            {
                get
                {
                    return _numWonByPlayer2;
                }
                set
                {
                    _numWonByPlayer2 = value;
                }
            }

            private int _pointsEarnedByPlayer1;

            public int PointsEarnedByPlayer1
            {
                get
                {
                    return _pointsEarnedByPlayer1;
                }
                set
                {
                    _pointsEarnedByPlayer1 = value;
                }
            }

            private int _pointsEarnedByPlayer2;

            public int PointsEarnedByPlayer2
            {
                get
                {
                    return _pointsEarnedByPlayer2;
                }
                set
                {
                    _pointsEarnedByPlayer2 = value;
                }
            }

            private int _pointsLostByPlayer1;

            public int PointsLostByPlayer1
            {
                get
                {
                    return _pointsLostByPlayer1;
                }
                set
                {
                    _pointsLostByPlayer1 = value;
                }
            }

            private int _pointsLostByPlayer2;

            public int PointsLostByPlayer2
            {
                get
                {
                    return _pointsLostByPlayer2;
                }
                set
                {
                    _pointsLostByPlayer2 = value;
                }
            }

        }

        public class AnalysePlayerResult
        {
            public AnalysePlayerResult()
            {
                _points = new List<double>();
            }

            private List<double> _points;

            public List<double> Points
            {
                get
                {
                    return _points;
                }
                set
                {
                    _points = value;
                }
            }

        }

        public class AuditTrailItem
        {
            private DateTime _when;

            public DateTime When
            {
                get
                {
                    return _when;
                }
                internal set
                {
                    _when = value;
                }
            }

            private string _what;

            public string What
            {
                get
                {
                    return _what;
                }
                internal set
                {
                    _what = value;
                }
            }

            private string _cssAttributes;

            public string CssAttributes
            {
                get
                {
                    return _cssAttributes;
                }
                internal set
                {
                    _cssAttributes = value;
                }
            }

        }
    }

}

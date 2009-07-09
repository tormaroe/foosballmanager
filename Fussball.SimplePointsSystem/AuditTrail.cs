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

        public AuditTrail()
        {
            _items = new List<AuditTrailItem>();
        }


        internal List<AuditTrailItem> Items
        {
            get
            {
                return _items;
            }
        }

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
            var matchAnalyser = new MatchAnalyser(this);
            return matchAnalyser.AnalyseMatches(playerName1, playerName2);
        }

        public AnalysePlayerResult AnalysePlayer(string playerName)
        {
            var playerAnalyser = new PlayerAnalyser(this);
            return playerAnalyser.Analyse(playerName);            
        }       
    }
}
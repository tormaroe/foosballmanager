using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class PlayerTable : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool visibleTemp = Visible;
            Refresh();
            Visible = visibleTemp;
        }

        public void Refresh()
        {
            Visible = true;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("GamesPlayed", typeof(int)));
            dt.Columns.Add(new DataColumn("SinglesWon", typeof(int)));
            dt.Columns.Add(new DataColumn("SinglesLost", typeof(int)));
            dt.Columns.Add(new DataColumn("DoublesWon", typeof(int)));
            dt.Columns.Add(new DataColumn("DoublesLost", typeof(int)));
            dt.Columns.Add(new DataColumn("Points", typeof(int)));

            DataRow dr;
            foreach (Player p in GetSortedPlayers())
            {
                dr = dt.NewRow();
                dr[0] = p.Name;
                dr[1] = p.GamesPlayed;
                dr[2] = p.SinglesWon;
                dr[3] = p.SinglesLost;
                dr[4] = p.DoublesWon;
                dr[5] = p.DoublesLost;
                dr[6] = p.Points;

                dt.Rows.Add(dr);
            }

            DataView dv = dt.DefaultView;
            //dv.Sort = "Points DESC";

            _grid.DataSource = dv;
            _grid.DataBind();

        }

        private List<Player> GetSortedPlayers()
        {
            List<Player> noGamesPlayed = new List<Player>();  // 0
            List<Player> fewGamesPlayed = new List<Player>();  // 1-9
            List<Player> manyGamesPlayed = new List<Player>();  // 10->
            foreach (Player p in PlayersUtil.ThePlayers.AllPlayers)
            {
                if (p.GamesPlayed == 0)
                    noGamesPlayed.Add(p);
                else if (p.GamesPlayed < 10)
                    fewGamesPlayed.Add(p);
                else
                    manyGamesPlayed.Add(p);
            }

            noGamesPlayed = GetSortedOnRating(noGamesPlayed);
            fewGamesPlayed = GetSortedOnRating(fewGamesPlayed);
            manyGamesPlayed = GetSortedOnRating(manyGamesPlayed);

            List<Player> sortedPlayers = new List<Player>();
            foreach (Player p in manyGamesPlayed)
            {
                sortedPlayers.Add(p);
            }
            foreach (Player p in fewGamesPlayed)
            {
                sortedPlayers.Add(p);
            }
            foreach (Player p in noGamesPlayed)
            {
                sortedPlayers.Add(p);
            }
            return sortedPlayers;
        }

        private List<Player> GetSortedOnRating(List<Player> players)
        {
            List<Player> sortedPlayers = new List<Player>();
            while (sortedPlayers.Count < players.Count)
            {
                Player bestSoFar = null;
                foreach (Player p in players)
                {
                    if (sortedPlayers.Contains(p))
                        continue;
                    if (bestSoFar == null)
                    {
                        bestSoFar = p;
                        continue;
                    }
                    if (p.Points > bestSoFar.Points)
                        bestSoFar = p;
                }
                sortedPlayers.Add(bestSoFar);
            }
            return sortedPlayers;
        }

    }
}
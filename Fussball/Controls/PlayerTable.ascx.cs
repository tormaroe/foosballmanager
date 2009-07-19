using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class PlayerTable : UserControl
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

            using (DataTable datatable = new DataTable())
            {
                AddTableColumns(datatable);                
                GetSortedPlayers().ForEach(player =>
                {
                    var datarow = datatable.NewRow();
                    AddPlayerToRow(player, datarow);
                    datatable.Rows.Add(datarow);
                });
                DataView dv = datatable.DefaultView;
                _grid.DataSource = dv;
            }
            _grid.DataBind();

        }

        private static void AddTableColumns(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("GamesPlayed", typeof(int)));
            dt.Columns.Add(new DataColumn("SinglesWon", typeof(int)));
            dt.Columns.Add(new DataColumn("SinglesLost", typeof(int)));
            dt.Columns.Add(new DataColumn("DoublesWon", typeof(int)));
            dt.Columns.Add(new DataColumn("DoublesLost", typeof(int)));
            dt.Columns.Add(new DataColumn("Points", typeof(int)));
        }

        private static void AddPlayerToRow(Player p, DataRow dr)
        {
            dr[0] = p.Name;
            dr[1] = p.GamesPlayed;
            dr[2] = p.SinglesWon;
            dr[3] = p.SinglesLost;
            dr[4] = p.DoublesWon;
            dr[5] = p.DoublesLost;
            dr[6] = p.Points;
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
            manyGamesPlayed.ForEach(sortedPlayers.Add);
            fewGamesPlayed.ForEach(sortedPlayers.Add);
            noGamesPlayed.ForEach(sortedPlayers.Add);
            
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
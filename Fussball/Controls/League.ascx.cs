using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fussball.SimplePointsSystem;
using System.Collections.Generic;

namespace Fussball.Controls
{
    public partial class League : UserControl
    {
        public delegate void LeagueMatchRemovedHandler(object sender, EventArgs e);

        public event LeagueMatchRemovedHandler LeagueMatchRemoved;

        protected void Page_Load(object sender, EventArgs e)
        {
            _settings.LeagueSettingsDone += _settings_LeagueSettingsDone;
        }

        void _settings_LeagueSettingsDone(object sender, EventArgs e)
        {
            ShowDefault();
        }

        public void Show()
        {
            Visible = true;
            ShowDefault();
        }
        public void Hide()
        {
            Visible = false;
        }

        protected void _btnViewSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void ShowDefault()
        {
            _settings.Hide();
            _leagueTablePlaceholder.Visible = true;

            _playersGrid.DataSource = SortByLeaguePoints(Fussball.SimplePointsSystem.League.Instance.Players.AllPlayers);
            _playersGrid.DataBind();

            _grid.DataSource = Fussball.SimplePointsSystem.League.Instance.Matches.AllMatches;
            _grid.DataBind();

        }

        private List<Player> SortByLeaguePoints(List<Player> players)
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
                    if (p.LeaguePoints > bestSoFar.LeaguePoints)
                        bestSoFar = p;
                }
                sortedPlayers.Add(bestSoFar);
            }
            return sortedPlayers;
        }

        private void ShowSettings()
        {
            _settings.Show();
            _leagueTablePlaceholder.Visible = false;
        }

        protected void _grid_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName.Equals("delete"))
            {
                Guid idToRemove = new Guid((string)e.CommandArgument);
                Fussball.SimplePointsSystem.League.Instance.ClearResult(idToRemove);

                if (LeagueMatchRemoved != null)
                {
                    LeagueMatchRemoved(this, e);
                }
            }
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fussball.Controls
{
    public partial class LeagueSettings : UserControl
    {
        private const int MINIMUM_LEAGUE_SIZE = 1;
        private const int MAXIMUM_LEAGUE_SIZE = 10;

        public delegate void LeagueSettingsDoneHandler(object sender, EventArgs e);

        public event LeagueSettingsDoneHandler LeagueSettingsDone;

        protected void _addPlayer_Click(object sender, EventArgs e)
        {
            Fussball.SimplePointsSystem.League.Instance.Players.Add(
                PlayersUtil.ThePlayers[new Guid(_playerToAdd.SelectedValue)]);
            PlayersUtil.SaveLeague();
            BindPlayersGrid();
        }

        protected void _btnGenerateMatches_Click(object sender, EventArgs e)
        {
            int leagueSize;
            if (Get_league_size_and_check_that_it_is_valid(out leagueSize))
            {
                int matchesGenerated = Fussball.SimplePointsSystem.League.Instance.GenerateMatches(leagueSize);                
                PlayersUtil.SaveLeague();

                _generateMessage.Text = string.Format("{0} games generated!", matchesGenerated);
            }
            else
            {
                _generateMessage.Text = String.Format("League size not a valid number. Valid range: {0} to {1}.",
                    MINIMUM_LEAGUE_SIZE,
                    MAXIMUM_LEAGUE_SIZE);
            }
        }

        private bool Get_league_size_and_check_that_it_is_valid(out int leagueSize)
        {
            return Int32.TryParse(_leagueSize.Text, out leagueSize)
                            && leagueSize >= MINIMUM_LEAGUE_SIZE
                            && leagueSize <= MAXIMUM_LEAGUE_SIZE;
        }

        protected void _playersGrid_Command(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Fussball.SimplePointsSystem.League.Instance.Players.Remove(new Guid((string)e.CommandArgument));
                PlayersUtil.SaveLeague();
                BindPlayersGrid();
            }
        }

        protected void _done_Click(object sender, EventArgs e)
        {

            if (LeagueSettingsDone != null)
            {
                LeagueSettingsDone(this, e);
            }
        }

        public void Show()
        {
            Visible = true;

            _playerToAdd.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _playerToAdd.DataTextField = "Name";
            _playerToAdd.DataValueField = "Id";
            _playerToAdd.DataBind();

            BindPlayersGrid();
        }

        private void BindPlayersGrid()
        {
            if (Fussball.SimplePointsSystem.League.Instance == null)
                return;

            _playersGrid.DataSource = Fussball.SimplePointsSystem.League.Instance.Players.AllPlayers;
            _playersGrid.DataBind();
        }

        public void Hide()
        {
            Visible = false;
        }
    }
}
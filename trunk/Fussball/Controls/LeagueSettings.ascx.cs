using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class LeagueSettings : System.Web.UI.UserControl
    {
        public delegate void LeagueSettingsDoneHandler(object sender, EventArgs e);

        public event LeagueSettingsDoneHandler LeagueSettingsDone;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _addPlayer_Click(object sender, EventArgs e)
        {
            //if (League.Instance == null)
            //{
            //    PlayersUtil.LoadLeague();
            //}

            Fussball.SimplePointsSystem.League.Instance.Players.Add(PlayersUtil.ThePlayers[new Guid(_playerToAdd.SelectedValue)]);
            PlayersUtil.SaveLeague();
            BindPlayersGrid();
        }

        protected void _btnGenerateMatches_Click(object sender, EventArgs e)
        {
            int leagueSize;
            if (Int32.TryParse(_leagueSize.Text, out leagueSize)
                && leagueSize > 0
                && leagueSize <= 10)
            {
                int matchesGenerated = Fussball.SimplePointsSystem.League.Instance.GenerateMatches(leagueSize);
                Fussball.SimplePointsSystem.League.Instance.ResetPlayerPoints();
                PlayersUtil.SaveLeague();

                _generateMessage.Text = string.Format("{0} games generated!", matchesGenerated);
            }
            else
            {
                _generateMessage.Text = "League size not a valid number. Valid range: 1 to 10.";
            }
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
            if (Fussball.SimplePointsSystem.League.Instance != null)
            {
                _playersGrid.DataSource = Fussball.SimplePointsSystem.League.Instance.Players.AllPlayers;
                _playersGrid.DataBind();
            }
        }

        public void Hide()
        {
            Visible = false;
        }
    }
}
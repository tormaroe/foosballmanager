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
    public partial class AddSingleMatch : System.Web.UI.UserControl
    {
        public delegate void SingleMatchAddedHandler(object sender, EventArgs e);
        public delegate void LeagueMatchAddedHandler(object sender, EventArgs e);

        public event SingleMatchAddedHandler SingleMatchAdded;
        public event LeagueMatchAddedHandler LeagueMatchAdded;

        protected void Page_Load(object sender, EventArgs e)
        {
            _messagePanel.Visible = false;
        }

        public void Show()
        {
            Visible = true;

            _winner.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _winner.DataTextField = "Name";
            _winner.DataValueField = "Id";
            _winner.DataBind();

            _looser.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _looser.DataTextField = "Name";
            _looser.DataValueField = "Id";
            _looser.DataBind();
        }

        public void Hide()
        {
            Visible = false;
        }

        protected void _btnAddMatch_Click(object sender, EventArgs e)
        {
            Guid winnerId = new Guid(_winner.SelectedValue);
            Guid looserId = new Guid(_looser.SelectedValue);

            if (winnerId.Equals(looserId))
            {
                ShowMessage("Can't play against yourself", "red");
                return;
            }

            Player winner = PlayersUtil.ThePlayers[winnerId];
            Player looser = PlayersUtil.ThePlayers[looserId];

            GameRegistration.RegisterSimpleGame(winner, looser);

            ShowMessage(string.Format("Game registered. Congratulations, {0}, you now have {1} points!",
                winner.Name,
                winner.Points),
                "green");

            if (_leagueMatch.Checked && Fussball.SimplePointsSystem.League.Instance.TryAddMatchResult(winner, looser))
            {
                Player leaguePlayerWinner = Fussball.SimplePointsSystem.League.Instance.Players[winner.Id];
                leaguePlayerWinner.LeaguePoints = leaguePlayerWinner.LeaguePoints + 1;
                leaguePlayerWinner.LeagueMatchesPlayed = leaguePlayerWinner.LeagueMatchesPlayed + 1;

                Player leaguePlayerLooser = Fussball.SimplePointsSystem.League.Instance.Players[looser.Id];
                leaguePlayerLooser.LeagueMatchesPlayed = leaguePlayerLooser.LeagueMatchesPlayed + 1;

                if (LeagueMatchAdded != null)
                {
                    LeagueMatchAdded(this, e);
                }
            }
            else
            {
                if (SingleMatchAdded != null)
                {
                    SingleMatchAdded(this, e);
                }
            }
            _leagueMatch.Checked = false;
        }

        private void ShowMessage(string text, string color)
        {
            _messagePanel.Visible = true;
            _message.Text = text;
            _messagePanel.Attributes["style"] = string.Format("color:{0};", color);
        }
    }
}
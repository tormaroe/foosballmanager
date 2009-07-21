using System;
using System.Web.UI;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class AddSingleMatch : UserControl
    {
        public event EventHandler<EventArgs> SingleMatchAdded;
        public event EventHandler<EventArgs> LeagueMatchAdded;

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

            HandleLeagueMatchAdding(winner, looser);
        }

        private void HandleLeagueMatchAdding(Player winner, Player looser)
        {
            if (_leagueMatch.Checked && Fussball.SimplePointsSystem.League.Instance.TryAddMatchResult(winner, looser))
            {
                if (LeagueMatchAdded != null)
                {
                    LeagueMatchAdded(this, EventArgs.Empty);
                }
            }
            else
            {
                if (SingleMatchAdded != null)
                {
                    SingleMatchAdded(this, EventArgs.Empty);
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
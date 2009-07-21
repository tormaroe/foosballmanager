using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class AddDoubleMatch : UserControl
    {
        public event EventHandler<EventArgs> DoubleMatchAdded;

        protected void Page_Load(object sender, EventArgs e)
        {
            _messagePanel.Visible = false;
        }

        public void Show()
        {
            Visible = true;

            BindDropdown(_winner1);
            BindDropdown(_winner2);
            BindDropdown(_looser1);
            BindDropdown(_looser2);
        }

        private static void BindDropdown(DropDownList dropDown)
        {
            dropDown.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            dropDown.DataTextField = "Name";
            dropDown.DataValueField = "Id";
            dropDown.DataBind();
        }

        public void Hide()
        {
            Visible = false;
        }

        protected void _btnAddMatch_Click(object sender, EventArgs e)
        {
            Guid winner1Id = new Guid(_winner1.SelectedValue);
            Guid winner2Id = new Guid(_winner2.SelectedValue);
            Guid looser1Id = new Guid(_looser1.SelectedValue);
            Guid looser2Id = new Guid(_looser2.SelectedValue);

            if (AnyonePickedMoreThanOnce(winner1Id, winner2Id, looser1Id, looser2Id))
            {
                ShowMessage("A player is picked more than once!!!", "red");
                return;
            }

            GameRegistration.RegisterDoubleGame(
                PlayersUtil.ThePlayers[winner1Id],
                PlayersUtil.ThePlayers[winner2Id],
                PlayersUtil.ThePlayers[looser1Id],
                PlayersUtil.ThePlayers[looser2Id]);

            ShowMessage("Game registered!", "green");

            if (DoubleMatchAdded != null)
            {
                DoubleMatchAdded(this, e);
            }
        }

        private bool AnyonePickedMoreThanOnce(Guid winner1Id, Guid winner2Id, Guid looser1Id, Guid looser2Id)
        {
            return winner1Id.Equals(winner2Id) 
                || winner1Id.Equals(looser1Id) 
                || winner1Id.Equals(looser2Id) 
                || winner2Id.Equals(looser1Id) 
                || winner2Id.Equals(looser2Id) 
                || looser1Id.Equals(looser2Id);
        }

        private void ShowMessage(string text, string color)
        {
            _messagePanel.Visible = true;
            _message.Text = text;
            _messagePanel.Attributes["style"] = string.Format("color:{0};", color);
        }
    }
}
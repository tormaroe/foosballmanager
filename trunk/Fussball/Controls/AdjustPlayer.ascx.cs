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
    public partial class AdjustPlayer : System.Web.UI.UserControl
    {
        public delegate void UserAdjustedHandler(object sender, EventArgs e);

        public event UserAdjustedHandler UserAdjusted;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Show()
        {
            Visible = true;
            _editPanel.Visible = false;

            _player.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _player.DataTextField = "Name";
            _player.DataValueField = "Id";
            _player.DataBind();
        }

        public void Hide()
        {
            Visible = false;
        }

        protected void _btnSelectPlayer_Click(object sender, EventArgs e)
        {
            Player p = PlayersUtil.ThePlayers[new Guid(_player.SelectedValue)];

            _id.Value = p.Id.ToString();
            _name.Text = p.Name;
            _singlesWonOriginal.Text = p.SinglesWon.ToString();
            _singlesLostOriginal.Text = p.SinglesLost.ToString();
            _doublesWonOriginal.Text = p.DoublesWon.ToString();
            _doublesLostOriginal.Text = p.DoublesLost.ToString();
            _pointsOriginal.Text = p.Points.ToString();
            _singlesWonNew.Text = p.SinglesWon.ToString();
            _singlesLostNew.Text = p.SinglesLost.ToString();
            _doublesWonNew.Text = p.DoublesWon.ToString();
            _doublesLostNew.Text = p.DoublesLost.ToString();
            _pointsNew.Text = p.Points.ToString();

            _editPanel.Visible = true;
        }

        protected void _update_Click(object sender, EventArgs e)
        {
            int newSinglesWon, newSinglesLost, newDoublesWon, newDoublesLost, newPoints;
            if (
                Int32.TryParse(_singlesWonNew.Text, out newSinglesWon) &&
                Int32.TryParse(_singlesLostNew.Text, out newSinglesLost) &&
                Int32.TryParse(_doublesWonNew.Text, out newDoublesWon) &&
                Int32.TryParse(_doublesLostNew.Text, out newDoublesLost) &&
                Int32.TryParse(_pointsNew.Text, out newPoints)
                )
            {
                Player p = PlayersUtil.ThePlayers[new Guid(_id.Value)];
                p.SinglesWon = newSinglesWon;
                p.SinglesLost = newSinglesLost;
                p.DoublesWon = newDoublesWon;
                p.DoublesLost = newDoublesLost;
                p.Points = newPoints;

                Fussball.SimplePointsSystem.AuditTrail.Instance.AddManualAudit(string.Format(
                    "Manual adjustment of player {0}: SW: {1}->{2}, SL: {3}->{4}, DW: {5}->{6}, DL: {7}->{8}, Points: {9}->{10}",
                    _name.Text,
                    _singlesWonOriginal.Text,
                    newSinglesWon,
                    _singlesLostOriginal.Text,
                    newSinglesLost,
                    _doublesWonOriginal.Text,
                    newDoublesWon,
                    _doublesLostOriginal.Text,
                    newDoublesLost,
                    _pointsOriginal.Text,
                    newPoints
                    ));


                if (UserAdjusted != null)
                {
                    UserAdjusted(this, e);
                }
            }
            else
            {
                throw new Exception("One of the new values can't be converted to a number");
            }
        }
    }
}
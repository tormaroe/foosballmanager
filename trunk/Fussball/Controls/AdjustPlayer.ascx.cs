using System;
using System.Web.UI;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class AdjustPlayer : UserControl
    {
        private int _newDoublesLost;
        private int _newDoublesWon;
        private int _newPoints;
        private int _newSinglesLost;
        private int _newSinglesWon;

        public delegate void UserAdjustedHandler(object sender, EventArgs e);

        public event UserAdjustedHandler UserAdjusted;

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
            Player p = GetPlayerFromIdString(_player.SelectedValue);

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

        private Player GetPlayerFromIdString(string id)
        {
            return PlayersUtil.ThePlayers[new Guid(id)];
        }

        protected void _update_Click(object sender, EventArgs e)
        {
            if (Parse_all_new_values_AND_they_are_all_valid())
            {
                UpdatePlayerWithNewValues();
                AddAuditTrailForAdjustment();

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

        private bool Parse_all_new_values_AND_they_are_all_valid()
        {
            return Int32.TryParse(_singlesWonNew.Text, out _newSinglesWon) &&
                            Int32.TryParse(_singlesLostNew.Text, out _newSinglesLost) &&
                            Int32.TryParse(_doublesWonNew.Text, out _newDoublesWon) &&
                            Int32.TryParse(_doublesLostNew.Text, out _newDoublesLost) &&
                            Int32.TryParse(_pointsNew.Text, out _newPoints);
        }

        private void UpdatePlayerWithNewValues()
        {
            Player p = GetPlayerFromIdString(_id.Value);
            p.SinglesWon = _newSinglesWon;
            p.SinglesLost = _newSinglesLost;
            p.DoublesWon = _newDoublesWon;
            p.DoublesLost = _newDoublesLost;
            p.Points = _newPoints;
        }

        private void AddAuditTrailForAdjustment()
        {
            Fussball.SimplePointsSystem.AuditTrail.Instance.AddManualAudit(string.Format(
                                "Manual adjustment of player {0}: SW: {1}->{2}, SL: {3}->{4}, DW: {5}->{6}, DL: {7}->{8}, Points: {9}->{10}",
                                _name.Text,
                                _singlesWonOriginal.Text,
                                _newSinglesWon,
                                _singlesLostOriginal.Text,
                                _newSinglesLost,
                                _doublesWonOriginal.Text,
                                _newDoublesWon,
                                _doublesLostOriginal.Text,
                                _newDoublesLost,
                                _pointsOriginal.Text,
                                _newPoints
                                ));
        }
    }
}
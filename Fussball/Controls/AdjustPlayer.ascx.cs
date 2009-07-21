using System;
using System.Web.UI;
using SPS = Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class AdjustPlayer : UserControl, IAdjustPlayerView
    {
        private AdjustPlayerController _controller;

        public event EventHandler<EventArgs> UserAdjusted;

        public AdjustPlayer()
        {
            _controller = new AdjustPlayerController(
                this, 
                PlayersUtil.ThePlayers, 
                SPS.AuditTrail.Instance);
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
            _id.Value = _player.SelectedValue;

            if (PlayerSelectedForAdjustments != null)
                PlayerSelectedForAdjustments(this, GetPlayerEventArgs());
        }

        private PlayerEventArgs GetPlayerEventArgs()
        {
            return new PlayerEventArgs(new Guid(_id.Value));
        }

        protected void _update_Click(object sender, EventArgs e)
        {
            if (AdjustPlayerRequest != null)
                AdjustPlayerRequest(this, GetPlayerEventArgs());

            if (UserAdjusted != null)
                UserAdjusted(this, e);
        }

        #region IAdjustPlayerView Members

        public event EventHandler<PlayerEventArgs> PlayerSelectedForAdjustments;
        public event EventHandler<PlayerEventArgs> AdjustPlayerRequest;

        public string PlayerName
        {
            get
            {
                return _name.Text;
            }
            set
            {
                _name.Text = value;
            }
        }

        public int DoublesWon
        {
            get
            {
                return Int32.Parse(_doublesWonNew.Text);
            }
            set
            {
                _doublesWonNew.Text = value.ToString();
                _doublesWonOriginal.Text = value.ToString();
            }
        }

        public int DoublesLost
        {
            get
            {
                return Int32.Parse(_doublesLostNew.Text);
            }
            set
            {
                _doublesLostNew.Text = value.ToString();
                _doublesLostOriginal.Text = value.ToString();
            }
        }

        public int SinglesWon
        {
            get
            {
                return Int32.Parse(_singlesWonNew.Text);
            }
            set
            {
                _singlesWonNew.Text = value.ToString();
                _singlesWonOriginal.Text = value.ToString();
            }
        }

        public int SignlesLost
        {
            get
            {
                return Int32.Parse(_singlesLostNew.Text);
            }
            set
            {
                _singlesLostNew.Text = value.ToString();
                _singlesLostOriginal.Text = value.ToString();
            }
        }

        public int Points
        {
            get
            {
                return Int32.Parse(_pointsNew.Text);
            }
            set
            {
                _pointsNew.Text = value.ToString();
                _pointsOriginal.Text = value.ToString();
            }
        }

        public void DisplayEditPanel()
        {
            _editPanel.Visible = true;
        }

        public bool IsValid()
        {
            int dummy;
            return Int32.TryParse(_singlesWonNew.Text, out dummy) &&
                            Int32.TryParse(_singlesLostNew.Text, out dummy) &&
                            Int32.TryParse(_doublesWonNew.Text, out dummy) &&
                            Int32.TryParse(_doublesLostNew.Text, out dummy) &&
                            Int32.TryParse(_pointsNew.Text, out dummy);
        }

        #endregion
    }
}
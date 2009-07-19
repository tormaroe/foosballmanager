using System;
using System.Web.UI;

namespace Fussball
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _addUserForm.UserAdded += _addUserForm_UserAdded;
            _addSingleMatchForm.SingleMatchAdded += _addSingleMatchForm_MatchAdded;
            _addDoubleMatchForm.DoubleMatchAdded += _addDoubleMatchForm_DoubleMatchAdded;
            _addSingleMatchForm.LeagueMatchAdded += _addSingleMatchForm_LeagueMatchAdded;
            _adjustPlayer.UserAdjusted += _adjustPlayer_UserAdjusted;
            _league.LeagueMatchRemoved += _league_LeagueMatchRemoved;
        }

        void _league_LeagueMatchRemoved(object sender, EventArgs e)
        {
            PlayersUtil.SaveLeague();
            _league.Show();
        }

        void _adjustPlayer_UserAdjusted(object sender, EventArgs e)
        {
            PlayersUtil.SavePlayersFile();
            PlayersUtil.SaveAuditTrail();
            _adjustPlayer.Hide();
            _playersTable.Refresh();
        }

        void _addSingleMatchForm_LeagueMatchAdded(object sender, EventArgs e)
        {
            PlayersUtil.SavePlayersFile();
            PlayersUtil.SaveAuditTrail();
            PlayersUtil.SaveLeague();
            _addSingleMatchForm.Visible = false;
            _playersTable.Visible = false;
            _league.Show();
        }

        void _addDoubleMatchForm_DoubleMatchAdded(object sender, EventArgs e)
        {
            PlayersUtil.SavePlayersFile();
            PlayersUtil.SaveAuditTrail();
            _addDoubleMatchForm.Visible = false;
            _playersTable.Refresh();
        }

        void _addUserForm_UserAdded(object sender, EventArgs e)
        {
            PlayersUtil.SavePlayersFile();
            PlayersUtil.SaveAuditTrail();
            _playersTable.Refresh();
            _addUserForm.Visible = false;
        }

        void _addSingleMatchForm_MatchAdded(object sender, EventArgs e)
        {
            PlayersUtil.SavePlayersFile();
            PlayersUtil.SaveAuditTrail();
            _addSingleMatchForm.Visible = false;
            _playersTable.Refresh();
        }

        #region menu click events

        protected void _btnPlayersTable_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Hide();
        }

        protected void _btnLeague_Click(object sender, EventArgs e)
        {
            _playersTable.Visible = false;
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Hide();
            _league.Show();
            _adjustPlayer.Hide();
        }

        protected void _btnNewPlayer_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = true;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Hide();
        }

        protected void _btnAddSingleMatch_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addDoubleMatchForm.Hide();
            _addSingleMatchForm.Show();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Hide();
        }

        protected void _btnAddDoubleMatch_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Show();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Hide();
        }

        protected void _btnAuditTrail_Click(object sender, EventArgs e)
        {
            _playersTable.Visible = false;
            _auditTrail.Refresh();
            _addUserForm.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Hide();
        }


        protected void _btnStats_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Show();
            _league.Hide();
            _adjustPlayer.Hide();
        }

        protected void _btnAdjustPlayer_Click(object sender, EventArgs e)
        {
            _playersTable.Refresh();
            _addUserForm.Visible = false;
            _auditTrail.Visible = false;
            _addSingleMatchForm.Hide();
            _addDoubleMatchForm.Hide();
            _stats.Hide();
            _league.Hide();
            _adjustPlayer.Show();
        }
        #endregion
    }
}

using System;
using System.Web.UI;

namespace Fussball.Controls
{
    public partial class AddUser : UserControl
    {
        public delegate void UserAddedHandler(object sender, EventArgs e);

        public event UserAddedHandler UserAdded;

        protected void _addUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_name.Text))
                return;

            PlayersUtil.ThePlayers.Add(new Fussball.SimplePointsSystem.Player(_name.Text));

            _name.Text = string.Empty;

            if (UserAdded != null)
            {
                UserAdded(this, e);
            }
        }
    }
}
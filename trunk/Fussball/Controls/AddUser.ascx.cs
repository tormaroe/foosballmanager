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

namespace Fussball.Controls
{
    public partial class AddUser : System.Web.UI.UserControl
    {
        public delegate void UserAddedHandler(object sender, EventArgs e);

        public event UserAddedHandler UserAdded;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _addUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_name.Text))
            {
                PlayersUtil.ThePlayers.Add(new Fussball.SimplePointsSystem.Player(_name.Text));

                _name.Text = string.Empty;

                if (UserAdded != null)
                {
                    UserAdded(this, e);
                }
            }
        }
    }
}
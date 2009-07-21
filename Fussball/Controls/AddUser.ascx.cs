using System;
using System.Web.UI;

namespace Fussball.Controls
{
    public partial class AddUser : UserControl, IAddUserView
    {
        private AddUserController _controller;

        public event EventHandler UserAdded;
        public event EventHandler AddUserRequest;

        public AddUser()
        {
            _controller = new AddUserController(this, PlayersUtil.ThePlayers);
        }
        
        public string Username
        {
            get { return _name.Text; }
            set { _name.Text = value; }
        }

        protected void _addUser_Click(object sender, EventArgs e)
        {
            if (AddUserRequest != null)
                AddUserRequest(null, null);
            
            if (UserAdded != null)            
                UserAdded(this, e);            
        }

        
    }
}
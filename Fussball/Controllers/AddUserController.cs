using System;
using Fussball.SimplePointsSystem;

namespace Fussball
{
    public class AddUserController
    {
        private Players _players;
        private IAddUserView _view;

        public AddUserController(IAddUserView view, Players players)
        {
            _players = players;
            _view = view;
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.AddUserRequest += OnAddUserRequest;
        }

        private void OnAddUserRequest(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_view.Username))
                return;

            _players.Add(new Player(_view.Username));

            _view.Username = string.Empty;
        }
    }
}

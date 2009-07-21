using System;

namespace Fussball
{
    public interface IAddUserView : IView
    {
        event EventHandler AddUserRequest;
        string Username { get; set; }
    }
}

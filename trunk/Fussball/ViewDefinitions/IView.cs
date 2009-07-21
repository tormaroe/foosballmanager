using System;

namespace Fussball
{
    public interface IView
    {
        event EventHandler Load;
        bool IsPostBack { get; }
        void DataBind();
    }
}
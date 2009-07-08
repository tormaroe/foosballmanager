<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDoubleMatch.ascx.cs" Inherits="Fussball.Controls.AddDoubleMatch" %>
<h3>Register a doubles match</h3>
<b>Winning team:</b><br /><br /> 
Player 1: <asp:DropDownList runat="server" ID="_winner1" />
Player 2: <asp:DropDownList runat="server" ID="_winner2" /><br /><br />
<b>Loosing team:</b><br /> <br />
Player 1: <asp:DropDownList runat="server" ID="_looser1" />
Player 2: <asp:DropDownList runat="server" ID="_looser2" /><br /><br />
<asp:Button runat="server" ID="_btnAddMatch" Text="Add the match" OnClick="_btnAddMatch_Click" />
<div runat="server" visible="false" id="_messagePanel">
    <br /><br />
    <asp:Label runat="server" ID="_message" />
</div>
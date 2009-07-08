<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSingleMatch.ascx.cs" Inherits="Fussball.Controls.AddSingleMatch" %>
<h3>Register a match</h3>
Winner: <asp:DropDownList runat="server" ID="_winner" />
Looser: <asp:DropDownList runat="server" ID="_looser" />
<asp:CheckBox runat="server" ID="_leagueMatch" Text="League match" />
<asp:Button runat="server" ID="_btnAddMatch" Text="Add the match" OnClick="_btnAddMatch_Click" />
<div runat="server" visible="false" id="_messagePanel">
    <br /><br />
    <asp:Label runat="server" ID="_message" />
</div>
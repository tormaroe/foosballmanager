<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUser.ascx.cs" Inherits="Fussball.Controls.AddUser" %>
<h3>Register new player</h3>
Player name:
<asp:TextBox runat="server" ID="_name" CssClass="textbox" />
<asp:Button runat="server" ID="_addUser" Text="Add user" CssClass="button" OnClick="_addUser_Click" />
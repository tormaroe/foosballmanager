<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AuditTrail.ascx.cs" Inherits="Fussball.Controls.AuditTrail" %>
<asp:Repeater runat="server" ID="_grid">
    <HeaderTemplate>
        <table width="100%" cellspacing="0">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td style="<%# Eval("Css") %>" width="150"><%# Eval("When") %>
            </td>
            <td style="<%# Eval("Css") %>" align="left"><%# Eval("What") %>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

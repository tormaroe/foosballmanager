<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stats.ascx.cs" Inherits="Fussball.Controls.Stats" %>
<h3>
    Competitor stats</h3>
Analyse matches between
<asp:DropDownList runat="server" ID="_player1" />
and
<asp:DropDownList runat="server" ID="_player2" />
<asp:Button runat="server" ID="_btnAddMatch" Text="Analyse" OnClick="_btnAnalyse_Click" />
<div runat="server" visible="false" id="_messagePanel">
    <br />
    <br />
    <asp:Label runat="server" ID="_message" />
    <br />
    <br />
    <table width="100%">
        <tr>
            <th>
                <asp:Label runat="server" ID="_player1Name" />
                trend chart
            </th>
            <th>
                <asp:Label runat="server" ID="_player2Name" />
                trend chart
            </th>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Image runat="server" ID="_chartPlayer1" /></td>
            <td style="text-align: center;">
                <asp:Image runat="server" ID="_chartPlayer2" /></td>
        </tr>
        <tr>
            <td style="text-align: center;">
                Max rating:
                <asp:Label runat="server" ID="_maxRatingPlayer1" /><br />
                Avg rating:
                <asp:Label runat="server" ID="_avgRatingPlayer1" /><br />
                Min rating:
                <asp:Label runat="server" ID="_minRatingPlayer1" /><br />
            </td>
            <td style="text-align: center;">
                Max rating:
                <asp:Label runat="server" ID="_maxRatingPlayer2" /><br />
                Avg rating:
                <asp:Label runat="server" ID="_avgRatingPlayer2" /><br />
                Min rating:
                <asp:Label runat="server" ID="_minRatingPlayer2" /><br />
            </td>
        </tr>
        <tr>
            <th colspan="2" style="text-align: center;">
                Rating trends combined:</th>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Image runat="server" ID="_chartCombined" />
            </td>
        </tr>
    </table>
</div>

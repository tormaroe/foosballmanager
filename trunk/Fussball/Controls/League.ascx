<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="League.ascx.cs" Inherits="Fussball.Controls.League" %>
<%@ Register Src="~/Controls/LeagueSettings.ascx" TagPrefix="fuss" TagName="LeagueSettings" %>
<fuss:LeagueSettings runat="server" ID="_settings" Visible="false" />
<asp:PlaceHolder runat="server" ID="_leagueTablePlaceholder" Visible="true">
    <table width="100%" style="border-bottom: solid 1px black;">
        <tr>
            <td align="left">
                <h3>
                    League matches</h3>
            </td>
            <td align="right">
                <asp:LinkButton runat="server" ID="_btnViewSettings" Text="League settings" OnClick="_btnViewSettings_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid runat="server" ID="_playersGrid" Width="100%" AutoGenerateColumns="false"
        CssClass="playerTable">
        <HeaderStyle BackColor="#93693d" ForeColor="#e5c889" />
        <AlternatingItemStyle BackColor="#e5c889" />
        <Columns>
            <asp:BoundColumn DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundColumn DataField="LeagueMatchesPlayed" HeaderText="Matches<br/>played"
                ItemStyle-Width="80" />
            <asp:BoundColumn DataField="LeaguePoints" HeaderText="League<br/>Points" ItemStyle-Width="80"
                ItemStyle-Font-Bold="true" />
            <asp:TemplateColumn HeaderText="Wins" ItemStyle-Width="80">
                <ItemTemplate>
                    <%# ((int)Eval("LeagueMatchesPlayed")) > 0 ? ((int)Eval("LeaguePoints") * 100 / (int)Eval("LeagueMatchesPlayed")).ToString() : "0" %>
                    %
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <br />
    <asp:DataGrid runat="server" ID="_grid" Width="100%" AutoGenerateColumns="false"
        CssClass="playerTable" OnItemCommand="_grid_ItemCommand">
        <HeaderStyle BackColor="#93693d" ForeColor="#e5c889" />
        <AlternatingItemStyle BackColor="#e5c889" />
        <Columns>
            <asp:BoundColumn DataField="PlayerName1" HeaderText="Player" ItemStyle-HorizontalAlign="Left" />
            <asp:TemplateColumn>
                <ItemTemplate>
                    vs</ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="PlayerName2" HeaderText="Player" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundColumn DataField="PlayedWhen" HeaderText="Played when" />
            <asp:BoundColumn DataField="Winner" HeaderText="Winner" />
            <asp:TemplateColumn HeaderText="Clear">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                        ImageUrl="~/images/delete.png" OnClientClick="return confirmAdminOperation('Are you sure you want to clear the result of this game?');" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:PlaceHolder>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeagueSettings.ascx.cs" Inherits="Fussball.Controls.LeagueSettings" %>
<h3>
    League settings</h3>
<asp:Panel ID="Panel1" runat="server" GroupingText="Players in league">
    <table width="100%">
        <tr>
            <td style="width: 40%" valign="top">
                <asp:DataGrid runat="server" ID="_playersGrid" Width="100%" AutoGenerateColumns="false"
                    CssClass="playerTable" OnItemCommand="_playersGrid_Command">
                    <HeaderStyle BackColor="#93693d" ForeColor="#e5c889" />
                    <AlternatingItemStyle BackColor="#e5c889" />
                    <Columns>
                        <asp:BoundColumn DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                                    ImageUrl="~/images/delete.png" OnClientClick="return confirmAdminOperation('Are you sure? This will mess up any league currently running!');" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>                
            </td>
            <td style="width: 60%" valign="top">
                <p style="text-align:left;margin-left:12px;">Note: After changing the user list you need to re-generate the league. All existing league results will be lost.</p>
                Add player to league:
                <asp:DropDownList runat="server" ID="_playerToAdd" />&nbsp;<asp:Button runat="server"
                    ID="_addPlayer" OnClick="_addPlayer_Click" Text="Add" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" GroupingText="The League">
    <p>A size of 1 will generate 2 matches between each player, size 2 twice as many and so on..</p>
    League size: <asp:TextBox runat="server"
        ID="_leagueSize" Width="50" />&nbsp;<asp:Button runat="server" ID="_btnGenerateMatches"
            Text="Generate matches" OnClick="_btnGenerateMatches_Click" OnClientClick="return confirmAdminOperation('Are you sure? This will permanently delete existing league statistics..');" />    
    <br /><br />
    <asp:Label runat="server" Text="" ID="_generateMessage" ForeColor="red" />
</asp:Panel>
<br />
<br />
<asp:Button runat="server" ID="_done" OnClick="_done_Click" Text="Done - go back to league table!" />
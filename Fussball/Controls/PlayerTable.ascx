<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlayerTable.ascx.cs" Inherits="Fussball.Controls.PlayerTable" %>
<asp:DataGrid runat="server" ID="_grid" Width="100%" AutoGenerateColumns="false" CssClass="playerTable" >
    <HeaderStyle BackColor="#93693d" ForeColor="#e5c889" />
    <AlternatingItemStyle BackColor="#e5c889" />
    <Columns>
        <asp:BoundColumn DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundColumn DataField="GamesPlayed" HeaderText="GamesPlayed" ItemStyle-Width="80" />
        <asp:BoundColumn DataField="SinglesWon" HeaderText="SinglesWon" ItemStyle-Width="80" ItemStyle-ForeColor="Green" />
        <asp:BoundColumn DataField="SinglesLost" HeaderText="SinglesLost" ItemStyle-Width="80" ItemStyle-ForeColor="Red" />
        <asp:BoundColumn DataField="DoublesWon" HeaderText="DoublesWon" ItemStyle-Width="80" ItemStyle-ForeColor="Green" />
        <asp:BoundColumn DataField="DoublesLost" HeaderText="DoublesLost" ItemStyle-Width="80" ItemStyle-ForeColor="Red" />
        <asp:BoundColumn DataField="Points" HeaderText="Points" ItemStyle-Width="80" />
    </Columns>
</asp:DataGrid>
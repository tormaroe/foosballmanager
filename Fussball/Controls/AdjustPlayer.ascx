<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdjustPlayer.ascx.cs" Inherits="Fussball.Controls.AdjustPlayer" %>
<h3>
    Fix player</h3>
Select player to edit:
<asp:DropDownList runat="server" ID="_player" />
<asp:Button runat="server" ID="_btnSelectPlayer" Text="Edit this player" OnClick="_btnSelectPlayer_Click" />
<asp:Panel runat="server" ID="_editPanel" GroupingText="Edit player details" Visible="false">
    <table width="100%">
        <tr>
            <td>Name:
            </td>
            <td><asp:Label runat="server" ID="_name" />
            </td>
            <td>&nbsp;<asp:HiddenField runat="server" ID="_id" />
            </td>
            <td rowspan="6" align="left" style="width:50%">
                Adjust the values and click update.<br /><br />
                Note: changing the player details don't change the AuditTrail. The Stats are 
                calculated from the AuditTrail, so they will also not be effected.<br /><br />
                <asp:Button runat="server" Text="Update" ID="_update" OnClick="_update_Click" OnClientClick="return confirmAdminOperation();" />
            </td>
        </tr>
        <tr>
            <td>Singles won:
            </td>
            <td><asp:Label runat="server" ID="_singlesWonOriginal" />
            </td>
            <td><asp:TextBox runat="server" ID="_singlesWonNew" Width="50" />
            </td>
        </tr>
        <tr>
            <td>Singles lost:
            </td>
            <td><asp:Label runat="server" ID="_singlesLostOriginal" />
            </td>
            <td><asp:TextBox runat="server" ID="_singlesLostNew" Width="50" />
            </td>
        </tr>
        <tr>
            <td>Doubles won:
            </td>
            <td><asp:Label runat="server" ID="_doublesWonOriginal" />
            </td>
            <td><asp:TextBox runat="server" ID="_doublesWonNew" Width="50" />
            </td>
        </tr>
        <tr>
            <td>Doubles lost:
            </td>
            <td><asp:Label runat="server" ID="_doublesLostOriginal" />
            </td>
            <td><asp:TextBox runat="server" ID="_doublesLostNew" Width="50" />
            </td>
        </tr>
        <tr>
            <td>Points:
            </td>
            <td><asp:Label runat="server" ID="_pointsOriginal" />
            </td>
            <td><asp:TextBox runat="server" ID="_pointsNew" Width="50" />
            </td>
        </tr>
    </table>
</asp:Panel>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fussball._Default" %>

<%@ Register Src="~/Controls/AddUser.ascx" TagPrefix="fuss" TagName="AddUser" %>
<%@ Register Src="~/Controls/PlayerTable.ascx" TagPrefix="fuss" TagName="PlayersTable" %>
<%@ Register Src="~/Controls/AddSingleMatch.ascx" TagPrefix="fuss" TagName="AddSingleMatch" %>
<%@ Register Src="~/Controls/AddDoubleMatch.ascx" TagPrefix="fuss" TagName="AddDoubleMatch" %>
<%@ Register Src="~/Controls/AuditTrail.ascx" TagPrefix="fuss" TagName="AuditTrailTable" %>
<%@ Register Src="~/Controls/Stats.ascx" TagPrefix="fuss" TagName="Stats" %>
<%@ Register Src="~/Controls/League.ascx" TagPrefix="fuss" TagName="League" %>
<%@ Register Src="~/Controls/AdjustPlayer.ascx" TagPrefix="fuss" TagName="AdjustPlayer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fussball manager</title>
    <link rel="stylesheet" media="screen" type="text/css" href="Fussball.css" />
    <%--<script language="javascript" type="text/javascript" src="scripts/fussballscripts.js" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="scripts/fussballscripts.js" />
            </Scripts>
        </asp:ScriptManager>
        <div style="text-align: center;">
            <div class="mainbox">
                <div class="header">
                </div>
                <div class="menu">
                    <asp:Button ID="_btnPlayersTable" runat="server" Text="Players table" CssClass="menuButton"
                        OnClick="_btnPlayersTable_Click" /><asp:Button ID="_btnLeague" runat="server" Text="League matches"
                            CssClass="menuButton" OnClick="_btnLeague_Click" /><asp:Button ID="_btnAddSingleMatch"
                                runat="server" Text="Add match (single)" CssClass="menuButton" OnClick="_btnAddSingleMatch_Click" /><asp:Button
                                    ID="_btnAddDoubleMatch" runat="server" Text="Add match (double)" CssClass="menuButton"
                                    OnClick="_btnAddDoubleMatch_Click" /><asp:Button ID="_btnNewPlayer" runat="server"
                                        Text="New player" CssClass="menuButton" OnClick="_btnNewPlayer_Click" /><asp:Button
                                            runat="server" ID="_btnAuditTrail" Text="AuditTrail" CssClass="menuButton" OnClick="_btnAuditTrail_Click" /><asp:Button
                                                runat="server" ID="_btnStats" Text="Stats" CssClass="menuButton" OnClick="_btnStats_Click" /><asp:Button
                                                    runat="server" ID="_btnAdjustPlayer" Text="Fix Player" CssClass="menuButton"
                                                    OnClick="_btnAdjustPlayer_Click" />
                </div>
                <div class="bodyframe">
                    <asp:UpdatePanel ID="_updateableBodyPanel" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="_btnPlayersTable" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnLeague" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnAddSingleMatch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnAddDoubleMatch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnNewPlayer" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnAuditTrail" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnStats" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="_btnAdjustPlayer" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <fuss:AddUser runat="server" ID="_addUserForm" Visible="false" />
                            <fuss:AddSingleMatch runat="server" ID="_addSingleMatchForm" Visible="false" />
                            <fuss:AddDoubleMatch runat="server" ID="_addDoubleMatchForm" Visible="false" />
                            <fuss:AuditTrailTable runat="server" ID="_auditTrail" Visible="false" />
                            <fuss:Stats runat="server" ID="_stats" Visible="false" />
                            <fuss:League runat="server" ID="_league" Visible="false" />
                            <fuss:AdjustPlayer runat="server" ID="_adjustPlayer" Visible="false" />
                            <fuss:PlayersTable runat="server" ID="_playersTable" Visible="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="footer">
                Version 2.0 - Made by Torbjørn Marø</div>
        </div>
    </form>
</body>
</html>

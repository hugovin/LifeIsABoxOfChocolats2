<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Run End Of Day Process</h2>

<table border="0" cellpadding="0" cellspacing="0" id="id-form">
        <tr>
            <th valign="top">
                Run UPS end of day Process:
            </th>
            <td>
                <input type="button" id="btnUps" value="" class="form-submit" />
            </td>
            <td>
            </td>
        </tr>
                <tr>
            <th valign="top">
                Run Encidia end of day Process:
            </th>
            <td>
                <input type="button" id="btnEncidia" value="" class="form-submit" />
            </td>
            <td>
            </td>
        </tr>



    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
    <script type="text/javascript" src="../../Scripts/EOD/index.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PageHeading" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CustomDialogs" runat="server">
    <div id="ups_dialog" title="UPS EOD completed">
        <p>UPS end of day process has been completed successfully</p>
    </div>
    <div id="Encidia_dialog" title="Encidia EOD completed">
        <p>Encidia end of day process has been completed successfully</p>
    </div>
</asp:Content>

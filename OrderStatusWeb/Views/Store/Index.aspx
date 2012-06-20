<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrderStatusWeb.Models.AllStores>" %>

<%@ Import Namespace="OrderStatusWeb.HtmlHelpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Stores Administration
</asp:Content>
<asp:Content ID="ContentHeding" ContentPlaceHolderID="PageHeading" runat="server">
    <h2>
        All Stores</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0" id="product-table">
        <tr>
            <th class="table-header-repeat line-left minwidth-1">
                <a href="">Store Name</a>
            </th>
            <th class="table-header-repeat line-left minwidth-1">
                <a href="">Store Url</a>
            </th>
            <th class="table-header-options line-left">
                <a href="">Options</a>
            </th>
        </tr>
        <% int isOdd = 0; %>
        <% foreach (var store in Model.ListOfStores)
           { %>
        <% if (isOdd == 0)
           {%>
        <tr>
            <% }
           else
           {%>
            <tr class="alternate-row">
                <%} %>
                <td>
                    <%:store.Name %>
                </td>
                <td>
                    <%: store.Url %>
                </td>
                <td class="options-width">
                    <a href="<%: Url.UpdateStore(store.Id.ToString())%>" title="Edit" class="icon-1 info-tooltip">
                    </a><a href="javascript:RemoveStore(<%: store.Id%>)" title="Delete" class="icon-2 info-tooltip">
                    </a>
                </td>
            </tr>
            <% if (isOdd == 0)
               {
                   isOdd++;
               }
               else
               {
                   isOdd--;
               } %>
            <%} %>
    </table>
    <br />
    <div class="buttons">
        <input type="button" value="" class="btnAddStore" onclick="javascript:location.href='<%: Url.AddStores()%>'">
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CustomDialogs" runat="server">
    <!-- ui-dialog -->
    <div id="remove_dialog" title="Remove Store">
        <p>
            Do you want to remove this store?</p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
    <script type="text/javascript" src="../../Scripts/Store/index.js"></script>
</asp:Content>

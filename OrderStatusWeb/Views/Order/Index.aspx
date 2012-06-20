<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrderStatusWeb.Models.OrderModels.AllOrders>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Orders in queue
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0" id="product-table">
        <tr>
            <th class="table-header-repeat line-left minwidth-1">
                <a href="">Order Id</a>
            </th>
            <th class="table-header-repeat line-left minwidth-1">
                <a href="">Invoice #</a>
            </th>
            <th class="table-header-repeat line-left">
                <a href="">Comment</a>
            </th>
            <th class="table-header-repeat line-left">
                <a href="">Last Update</a>
            </th>
            <th class="table-header-options line-left">
                <a href="">Options</a>
            </th>
        </tr>
        <% int isOdd = 0;
                   foreach (var order in Model.Orders)
                     {
                       if(isOdd ==0)
                         {%>
        <tr>
            <% isOdd++;
                         }else
                         {%>
            <tr class="alternate-row">
                <% isOdd--;
                         }%>
                <td>
                    <%:order.OrderId %>
                </td>
                <td>
                    <%:order.InvoiceNumber %>
                </td>
                <td>
                    <%:order.OrderComment %>
                </td>
                <td>
                    <%:order.LastUpdate.ToShortDateString() %>
                </td>
                <td class="options-width">
                    <a href="" title="Edit" class="icon-1 info-tooltip"></a><a href="" title="Edit" class="icon-2 info-tooltip">
                    </a><a href="" title="Edit" class="icon-3 info-tooltip"></a><a href="" title="Edit"
                        class="icon-4 info-tooltip"></a><a href="" title="Edit" class="icon-5 info-tooltip">
                        </a>
                </td>
            </tr>
            <%  } %>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageHeading" runat="server">
    Orders in queue
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CustomDialogs" runat="server">
</asp:Content>

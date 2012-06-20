<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OrderStatusWeb.Models.StoreModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditStore
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <!-- start id-form -->
        <%: Html.HiddenFor(model => model.StoreId) %>
    <table border="0" cellpadding="0" cellspacing="0" id="id-form">
        <tr>
            <th valign="top">
                Store Name:
            </th>
            <td>
                <%: Html.TextBoxFor(model => model.StoreName, new { @class = "inp-form", placeholder = "Store Name" })%>
                <%: Html.ValidationMessageFor(model => model.StoreName)%>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th valign="top">
                Store URL:
            </th>
            <td>
                <%: Html.TextBoxFor(model => model.Url, new { @class = "inp-form", placeholder = "www.address.com" })%>
                <%: Html.ValidationMessageFor(model => model.Url)%>
            </td>
        </tr>
        <tr>
            <th valign="top">
                API key:
            </th>
            <td>
                <%: Html.TextBoxFor(model => model.ApiKey, new { @class = "inp-form", placeholder = "32 numbers" })%>
                <%: Html.ValidationMessageFor(model => model.ApiKey) %>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th valign="top">
                InterVal:
            </th>
            <td>
                <%= Html.DropDownListFor(x => x.DefaultInterval, Model.IntervalList, new { @class = "intervalDD" })%>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th valign="top">
                OrderStatus:
            </th>
            <td>
                  <%
                    string[] listOfIds = Model.listOfIds.Split(',');
                      foreach (var orderStatus in Model.OrderStatus)

                    {
                        if (listOfIds.Contains(orderStatus.Id.ToString()))
                        {%>
                        <input type="checkbox" class="checkboxDummy" name="rdOrderStatus" checked="checked" value="<%= orderStatus.Id %>" /><label><%: orderStatus.Text %> </label> &nbsp;|&nbsp;
                         <% }
                        else
                        {%>
                            <input type="checkbox" class="checkboxDummy" name="rdOrderStatus" value="<%= orderStatus.Id %>" /><label><%: orderStatus.Text %> </label> &nbsp;|&nbsp;
                        <%}%>                    
                   <% } %>  
                   <%: Html.ValidationMessageFor(model=> model.OrderStatus) %>
                   <%:Html.LabelFor(model => model.CustomOrderStatus) %>
                    <%: Html.TextBoxFor(model => model.CustomOrderStatus, new { @class = "inp-form" })%>
                    <%: Html.ValidationMessageFor(model => model.CustomOrderStatus)%>   
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                Custom Shipments
            </th>
            <td valign="top">
                <ul id="ulCustomShipments" class="noBullets">
                    <li id="liCus1" class="">
                        <%: Html.TextBoxFor(model => model.CustomShip1, new { @class = "inp-form", placeholder = "Carrier - Description" })%></li>
                    <li id="liCus2" class="dn">
                        <%: Html.TextBoxFor(model => model.CustomShip2, new { @class = "inp-form", placeholder = "Carrier - Description" })%><span
                            id="spnCustomShip2" class="icon-2 "></span></li>
                    <li id="liCus3" class="dn">
                        <%: Html.TextBoxFor(model => model.CustomShip3, new { @class = "inp-form", placeholder = "Carrier - Description" })%><span
                            id="spnCustomShip3" class="icon-2 "></span></li>
                    <li><span id="spAddmore">+Add More</span></li>
                </ul>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td valign="top">
                <input type="submit" value="" class="form-submit" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <% } %>

    <!-- end id-form  -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
     <script type="text/javascript">
        var CustomShipments = <%:Model.CustomShipments.Count%>;
    </script>
    <script type="text/javascript" src="../../Scripts/Store/EditStore.js"></script>
</asp:Content>

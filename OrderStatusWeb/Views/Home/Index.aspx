<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OrderStatusWeb.Models.HomeModels>" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Pull Orders</h2>
   <table border="0" cellpadding="0" cellspacing="0" id="id-form">
        <tr class="homeTableTr">
            <th valign="top" class="homeTable">
                <label for="btnOrders">Pull all orders from the registered stores</label> 
            </th>
            <td>
                <input type="button" id="btnOrders" value="" class="form-submit">           
            </td>
            <td>
            </td>
        </tr>
        <tr class="homeTableTr">
            <th valign="top" class="homeTable">
            <span>Or pull an specific order from an store</span>  
            </th>
            <td>
                  
            </td>
        </tr>
        <tr class="homeTableTr">
            <th valign="top" class="homeTable">
                <%= Html.LabelFor(x => x.SelectedStore)%>
            </th>
            <td>
                 <%= Html.DropDownListFor(x => x.SelectedStore, Model.StoreList, new { @class = "intervalDD" })%>  
            </td>
        </tr>
        <tr class="homeTableTr">
            <th valign="top" class="homeTable">
                <label for="txtInvoice">Invoice#</label>
            </th>
            <td>
                <input type="text" id="txtInvoice" name="txtInvoice" maxlength="15" class="inp-form"/>
            </td>
        </tr>
        <tr class="homeTableTr">
            <th valign="top" class="homeTable">

            </th>
            <td>
                <input type="button" id="btnSingleOrder" value="" class="form-submit">
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
    <script type="text/javascript" src="../../Scripts/Home/home.js"></script>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="CustomDialogs" runat="server">
    <div id="AllOrders" title="Pull Orders">
        <p>Orders have been extracted from the sellers listed</p>
    </div>
    <div id="SingleOrder" title="Pull Order">
        <span id="orderResult"></span>
    </div>

</asp:Content>

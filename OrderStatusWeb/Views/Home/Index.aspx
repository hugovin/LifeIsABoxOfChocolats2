<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="divPullOrders">
     <div class="divPullOrdersLabel"> Pull orders from the registered stores </div><div class="divPullOrdersButton"><input type="button" id="btnOrders" value="" class="form-submit"></div>
  </div>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
    <script type="text/javascript" src="../../Scripts/Home/home.js"></script>
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="CustomDialogs" runat="server">
    <div id="AllOrders" title="Pull Orders">
        <p>Orders have been extracted from the sellers listed</p>
    </div>

</asp:Content>

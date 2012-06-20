<%@ Page Language="C#" MasterPageFile="~/Views/Shared/LogIn.Master" Inherits="System.Web.Mvc.ViewPage<OrderStatusWeb.Models.LogOnModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Log On</h2>

    <% using (Html.BeginForm("LogOn", "Account"))
       { %>

    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                <%: Html.LabelFor(m => m.UserName) %>
            </th>
            <td>
                <%: Html.TextBoxFor(m => m.UserName, new { @class = "login-inp" })%>
                <%: Html.ValidationMessageFor(m => m.UserName) %>
            </td>
        </tr>
        <tr>
            <th>
                <%: Html.LabelFor(m => m.Password) %>
            </th>
            <td>
                <%: Html.PasswordFor(m => m.Password, new { @class = "login-inp" })%>
                <%: Html.ValidationMessageFor(m => m.Password) %>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td valign="top">
                <%: Html.ValidationSummary(true, "") %> <br/>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <input type="submit" class="submit-login" />
            </td>
        </tr>
    </table>
    <% } %>
</asp:Content>

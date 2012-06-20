using System.Web;
using System.Web.Mvc;

namespace OrderStatusWeb.HtmlHelpers
{
    public static class UrlHelperExtension
    {
        #region StoreHtmlHelper

        public static string AddStores(this UrlHelper helper)
        {
            return helper.Action("AddStore", "Store");
        }

        public static string UpdateStore(this UrlHelper helper,string id)
        {
            return helper.Action("EditStore", "Store", new { id = id });
        }

        #endregion
    }
}
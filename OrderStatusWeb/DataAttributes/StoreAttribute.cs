using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderStatusWeb.DataAttributes
{
    public class StoreAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!HttpContext.Current.Session["Role"].Equals("Admin"))
            {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "Home",
                        area = "",
                        ReturnUrl = filterContext.HttpContext.Request.CurrentExecutionFilePath
                    }));
            }
        }
    }
}
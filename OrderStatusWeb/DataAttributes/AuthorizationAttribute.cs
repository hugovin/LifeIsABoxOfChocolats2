using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderStatusWeb.DataAttributes
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (HttpContext.Current.Session["UserName"] == null)
            {
                if (!HttpContext.Current.Request.Path.Equals("/"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "LogOn",
                        controller = "Account",
                        area = "",
                        ReturnUrl = filterContext.HttpContext.Request.CurrentExecutionFilePath
                    }));
                }
            }
        }
    }
}
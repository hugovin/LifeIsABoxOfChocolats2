using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderStatusCore;
using OrderStatusWeb.DataAttributes;

namespace OrderStatusWeb.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to the Store Administrator Tool";
            
            return View();
        }

        public ActionResult About()
        {
            
            return View();
        }
    }
}

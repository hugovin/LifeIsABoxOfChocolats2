using System.Web.Mvc;
using OrderStatusCore;

namespace OrderStatusWeb.Controllers
{
    public class EODController : Controller
    {
        //
        // GET: /EOD/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RunOrdersEod()
        {
            EOD eod = new EOD();
            return Json(eod.OrdersEOD(), JsonRequestBehavior.AllowGet);
        }
    }
}

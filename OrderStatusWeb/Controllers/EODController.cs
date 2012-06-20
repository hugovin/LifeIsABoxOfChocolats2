using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
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

        public JsonResult RunUpsEod()
        {
            EOD eod = new EOD();
            return Json(eod.UpsEOD(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RunEncidiaEod()
        {
            EOD eod = new EOD();
            return Json(eod.UspsEOD(), JsonRequestBehavior.AllowGet);
        }



    }
}

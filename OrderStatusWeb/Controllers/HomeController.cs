using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderStatusCore;
using OrderStatusCore.DataTransferObjects;
using OrderStatusWeb.DataAttributes;
using OrderStatusWeb.Models;

namespace OrderStatusWeb.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            HomeModels homeModels = new HomeModels();
            Stores stores = new Stores();
            List<StoreDto> listOfStores = stores.GetAllStores();
            homeModels.StoreList = GenerateSelectListItems(listOfStores);
            ViewData["Message"] = "Welcome to the Store Administrator Tool";
            
            return View(homeModels);
        }

        public ActionResult About()
        {
            
            return View();
        }

        public  JsonResult PullOrders()
        {
            Stores stores = new Stores();
            stores.CheckAllOrders();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PullAnOrder(int storeId, string invoiceNumber)
        {
            Stores stores = new Stores();
            return Json(stores.CheckForAnOrder(storeId,invoiceNumber), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GenerateSelectListItems(IEnumerable<StoreDto> intervals)
        {
            return from i in intervals
                   select new SelectListItem
                   {
                       Text = i.Name,
                       Value = i.Id.ToString()
                   };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderStatusCore;
using OrderStatusWeb.Models;

namespace OrderStatusWeb.Controllers
{
    public class OrderController : Controller
    {
        private Orders ordersCore = new Orders();
        //
        // GET: /Order/

        public ActionResult Index()
        {
            OrderModels.AllOrders model = new OrderModels.AllOrders();
            var ListOfOrders = ordersCore.GetAllOrdersToProcess();
            if (ListOfOrders != null)
            {
                model.Orders = ListOfOrders;
            }

            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderStatusCore.DataTransferObjects;
using OrderStatusData.DataTransferObjects;
using OrderStatusWeb.DataAttributes;
using OrderStatusCore;
using OrderStatusWeb.Models;

namespace OrderStatusWeb.Controllers
{
    public class StoreController : Controller
    {
        private static List<string> intervalsList = new List<string>(new string[] { "2", "4", "6" });
        Stores _stores = null;
        //
        // GET: /Stores/
        [Authorization]
        public ActionResult Index()
        {
            _stores = new Stores();
            AllStores model = new AllStores();
            model.ListOfStores = _stores.GetAllStores();
            return View(model);
        }

        [Authorization]
        public ActionResult AddStore()
        {
            _stores = new Stores();
            StoreModels store = new StoreModels();
            store.IntervalList = GenerateSelectListItems(intervalsList,"");
            store.OrderStatus = _stores.GetPublicOrdersStatus();
            return View(store);
        }

        private IEnumerable<SelectListItem> GenerateSelectListItems(IEnumerable<string> intervals, string interval)
        {
            return from i in intervals
                   select new SelectListItem
                   {
                       Selected = (interval != null && interval.Equals(i)) ? true : false,
                       Text = i,
                       Value = i
                   };
        }

        [Authorization]
        [HttpPost]
        public ActionResult AddStore(StoreModels store)
        {
            _stores = new Stores();
            if (ModelState.IsValid)
            {
                if (!Utilities.RegularExpressions.UrlIsValid(store.Url))
                {
                    ModelState.AddModelError("URL", "Invalid Url");
                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    return View(store);
                }
                if (!Utilities.RegularExpressions.ApiKeyIsvalid(store.ApiKey))
                {
                    
                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    ModelState.AddModelError("ApiKey", "The API KEY must be a 32 characters long and ONLY numbers");
                    return View(store);
                }
                string[] CharList = Request.Form["rdOrderStatus"].Split(',');
                if (CharList.Length == 0 && store.CustomOrderStatus.Equals(""))
                {
                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    ModelState.AddModelError("OrderStatus", "You need to select at least one Order Status or create your custom one");
                    return View(store);
                }
                List<string> orderStatusList = new List<string>();
                foreach (string item in CharList)
                {
                    orderStatusList.Add(item);
                }

                StoreDto storeDto = new StoreDto();
                storeDto.Name = store.StoreName;
                storeDto.ApiKey = store.ApiKey;
                storeDto.Url = store.Url;
                storeDto.Interval = int.Parse(store.DefaultInterval);

                string customOrderStatus = "";
                if (store.CustomOrderStatus != null && !store.CustomOrderStatus.Equals(""))
                {
                    customOrderStatus = _stores.AddCustomOrderStatus(store.CustomOrderStatus).ToString();
                }


                if (!customOrderStatus.Equals(""))
                {
                    orderStatusList.Add(customOrderStatus);
                }

                int storeId = _stores.AddStore(storeDto);
                if (storeId != 0)
                {
                    _stores.AddOrderStatusByStoreId(orderStatusList, storeId);
                    List<string> customShipments = new List<string>();
                    if (store.CustomShip1 != null && !store.CustomShip1.Equals(""))
                    {
                        customShipments.Add(store.CustomShip1);
                    }
                    if (store.CustomShip2 != null && !store.CustomShip2.Equals(""))
                    {
                        customShipments.Add(store.CustomShip2);
                    }
                    if (store.CustomShip3 != null && !store.CustomShip3.Equals(""))
                    {
                        customShipments.Add(store.CustomShip3);
                    }
                    if (customShipments.Count>0)
                    {
                        _stores.CreateCustomShipments(customShipments,storeId);
                    }
                    return RedirectToAction("Index", "Store");
                }
                return RedirectToAction("Index", "Store");
            }
            else
            {
                store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                store.OrderStatus = _stores.GetPublicOrdersStatus();
                return View(store);
            }


        }

        [Authorization]
        public ActionResult EditStore(string id)
        {
            _stores = new Stores();
            
            StoreDto dto = _stores.GetStoreById(int.Parse(id));
            if (dto != null)
            {
                StoreModels model = new StoreModels();
                model.StoreId = int.Parse(id);
                model.ApiKey = dto.ApiKey;
                model.StoreName = dto.Name;
                model.Url = dto.Url;
                model.IntervalList = GenerateSelectListItems(intervalsList, dto.Interval.ToString());
                model.OrderStatusIds = _stores.GetAllPublicOrderStatusByStore(dto.Id);
                model.CustomOrderStatus = _stores.GetNonPublicOrderStatusByStore(dto.Id);
                List<CustomShipmentsDto> list = _stores.GetCustomShipmentByStoreId(dto.Id);
                model.CustomShipments = list;
                model.OrderStatus = _stores.GetPublicOrdersStatus();
               
                foreach (var orde in model.OrderStatusIds)
                {
                    model.listOfIds += orde + ",";
                }
                if (model.listOfIds != null && !model.listOfIds.Equals(""))
                {
                    model.listOfIds = model.listOfIds.Remove(model.listOfIds.Count() - 1);
                }
                
                int counter = 0;
                foreach (var customShipmentsDto in list)
                {
                    if (counter == 0)
                    {
                        model.CustomShip1 = customShipmentsDto.Name;
                    }
                    if (counter == 1)
                    {
                        model.CustomShip2 = customShipmentsDto.Name;
                        
                    }
                    if (counter == 2)
                    {
                        model.CustomShip3 = customShipmentsDto.Name;
                    }
                    counter++;
                }
                return View(model);
            }
            return RedirectToAction("Index", "Store");
            
        }

        [Authorization]
        [HttpPost]
        public ActionResult EditStore(StoreModels store)
        {
            _stores = new Stores();
            if (ModelState.IsValid)
            {
                if (!Utilities.RegularExpressions.UrlIsValid(store.Url))
                {
                    ModelState.AddModelError("URL", "Invalid Url");
                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    return View(store);
                }
                if (!Utilities.RegularExpressions.ApiKeyIsvalid(store.ApiKey))
                {

                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    ModelState.AddModelError("ApiKey", "The API KEY must be a 32 characters long and ONLY numbers");
                    return View(store);
                }
                string[] CharList = Request.Form["rdOrderStatus"] != null ?  Request.Form["rdOrderStatus"].Split(','): new string[0];
                if (CharList.Length == 0 && store.CustomOrderStatus.Equals(""))
                {
                    store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                    store.OrderStatus = _stores.GetPublicOrdersStatus();
                    ModelState.AddModelError("OrderStatus", "You need to select at least one Order Status or create your custom one");
                    return View(store);
                }
                List<string> orderStatusList = new List<string>();
                foreach (string item in CharList)
                {
                    orderStatusList.Add(item);
                }

                StoreDto storeDto = new StoreDto();
                storeDto.Id = store.StoreId;
                storeDto.Name = store.StoreName;
                storeDto.ApiKey = store.ApiKey;
                storeDto.Url = store.Url;
                storeDto.Interval = int.Parse(store.DefaultInterval);

                string customOrderStatus = "";
                if (store.CustomOrderStatus != null && !store.CustomOrderStatus.Equals(""))
                {
                    customOrderStatus = _stores.AddCustomOrderStatus(store.CustomOrderStatus).ToString();
                }


                if (!customOrderStatus.Equals(""))
                {
                    orderStatusList.Add(customOrderStatus);
                }

                int storeId = _stores.EditStore(storeDto);
                if (storeId != 0)
                {
                    _stores.AddOrderStatusByStoreId(orderStatusList, storeId);
                    List<string> customShipments = new List<string>();
                    if (store.CustomShip1 != null && !store.CustomShip1.Equals(""))
                    {
                        customShipments.Add(store.CustomShip1);
                    }
                    if (store.CustomShip2 != null && !store.CustomShip2.Equals(""))
                    {
                        customShipments.Add(store.CustomShip2);
                    }
                    if (store.CustomShip3 != null && !store.CustomShip3.Equals(""))
                    {
                        customShipments.Add(store.CustomShip3);
                    }
                    if (customShipments.Count > 0)
                    {
                        _stores.CreateCustomShipments(customShipments, storeId);
                    }
                    return RedirectToAction("Index", "Store");
                }
                return RedirectToAction("Index", "Store");
            }
            else
            {
                store.IntervalList = GenerateSelectListItems(intervalsList, store.DefaultInterval);
                store.OrderStatus = _stores.GetPublicOrdersStatus();
                return View(store);
            }

            return RedirectToAction("Index", "Store");
        }


        [Authorization]
        public JsonResult Remove(string id)
        {
            _stores = new Stores();
            return Json(_stores.RemoveStore(int.Parse(id)), JsonRequestBehavior.AllowGet);
        }

    }
}

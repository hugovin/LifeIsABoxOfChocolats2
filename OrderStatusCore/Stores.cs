using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using OrderStatusCore.DataTransferObjects;
using OrderStatusData;
using OrderStatusData.DataTransferObjects;
using OrderStatusData.UPS;
using OrderStatusCore.API_3dCart;
using System.Data;
using System.Xml;

using Newtonsoft.Json;

namespace OrderStatusCore
{
    public class Stores
    {
        private Orders orders = new Orders();
        public List<StoreDto> GetAllStores()
        {
            var data = new orderstatusEntities();
            List<StoreDto> listOfStores = new List<StoreDto>();
            try {
                var ts = "";
                var stores = data.stores_data.Where(x=> x.isActive == 1).ToList();
                foreach (var store in stores)
                {
                    listOfStores.Add(new StoreDto
                                        {
                                             Id = store.id,
                                             Name = store.name,
                                             ApiKey = store.api_key,
                                             ApiUser = "",
                                             Url = store.url,
                                             LastRun = (DateTime)store.last_run
                                        });
                }
            }
            catch (InvalidOperationException exc)
            {
                return null;
            }
            catch (ArgumentNullException exc)
            {
                return null;
            }
            catch (NullReferenceException exc)
            {
                return null;
            }
            catch (OptimisticConcurrencyException exc)
            {
                return null;
            }
            catch (UpdateException exc)
            {
                return null;
            }
            finally
            {
                data.Dispose();
            }
            return listOfStores;
        }

        public string CheckForAnOrder(int storeId, string invoice)
        {
            if (CheckIfOrderExist(invoice))
            {
                return "Invoice "+invoice+ " is already on the records";
            }
            cartAPI api = new cartAPI();
            var store = GetStoreById(storeId);
            XDocument xdoc = XDocument.Parse(api.getOrder(store.Url, store.ApiKey, 100, 1, false,invoice, "", "", "", "").OuterXml);
            if (!xdoc.Root.Name.LocalName.Equals("Error"))
            {
                var OrderInformation = from x in xdoc.Descendants("Order")
                                       select new
                                       {
                                           //---OrderInformation 
                                           OrderID = x.Descendants("OrderID").First().Value,
                                           Total = x.Descendants("Total").First().Value,
                                           InvoiceNumber = x.Descendants("InvoiceNumber").First().Value,
                                           CustomerID = x.Descendants("CustomerID").First().Value,
                                           OrderStatus = x.Descendants("OrderStatus").First().Value,
                                           DateStarted = x.Descendants("DateStarted").First().Value,
                                           LastUpdate = x.Descendants("LastUpdate").First().Value,
                                           OrderComment = x.Descendants("Comments").Descendants("OrderComment").First().Value,
                                           OrderIntComment = x.Descendants("Comments").Descendants("OrderInternalComment").First().Value,
                                           OrderExtComment = x.Descendants("Comments").Descendants("OrderExternalComment").First().Value,
                                           Shippinginfo = x.Descendants("ShippingInformation").Descendants("Shipment"),
                                           ItemInfo = x.Descendants("ShippingInformation").Descendants("OrderItems").Descendants("Item"),
                                           Email = x.Descendants("BillingAddress").Descendants("Email").First().Value

                                       };
                var result = orders.CreateOrdersFromStore(OrderInformation, store.Id);
                
                 return "Invoice "+invoice+ " has been added succesfully";
            }
            else
            {
                return "Invoice "+invoice+ " was not found in the selected store. Try another store";
            }
            
        }

        public void CheckAllOrders()
        {
            
            List<StoreDto> listOfStores = GetAllStores();
            cartAPI api = new cartAPI();
            //UpsDbMethods upsDb = new UpsDbMethods();
            //bool rer = upsDb.ReadAllUpsData();
            //Calls the authorization url 
            if (listOfStores != null)
            {
                foreach (var st in listOfStores)
                {
                    List<string> OrderStatuses = GetAllOrderStatusTextByStore(st.Id);
                    foreach (var orderStatuse in OrderStatuses)
                    {
                        string lastRun = String.Format("{0:MM/dd/yyyy}", st.LastRun);
                        //string status = GetOrderStatusText(st.OrderStatus);
                        XDocument xdoc = XDocument.Parse(api.getOrder(st.Url, st.ApiKey, 100, 1, false, "", orderStatuse, lastRun, "", "").OuterXml);
                        if (!xdoc.Root.Name.LocalName.Equals("Error"))
                        {
                            var OrderInformation = from x in xdoc.Descendants("Order")
                                                   select new
                                                   {
                                                       //---OrderInformation 
                                                       OrderID = x.Descendants("OrderID").First().Value,
                                                       Total = x.Descendants("Total").First().Value,
                                                       InvoiceNumber = x.Descendants("InvoiceNumber").First().Value,
                                                       CustomerID = x.Descendants("CustomerID").First().Value,
                                                       OrderStatus = x.Descendants("OrderStatus").First().Value,
                                                       DateStarted = x.Descendants("DateStarted").First().Value,
                                                       LastUpdate = x.Descendants("LastUpdate").First().Value,
                                                       OrderComment = x.Descendants("Comments").Descendants("OrderComment").First().Value,
                                                       OrderIntComment = x.Descendants("Comments").Descendants("OrderInternalComment").First().Value,
                                                       OrderExtComment = x.Descendants("Comments").Descendants("OrderExternalComment").First().Value,
                                                       Shippinginfo = x.Descendants("ShippingInformation").Descendants("Shipment"),
                                                       ItemInfo = x.Descendants("ShippingInformation").Descendants("OrderItems").Descendants("Item"),
                                                       Email = x.Descendants("BillingAddress").Descendants("Email").First().Value

                                                   };
                            var result = orders.CreateOrdersFromStore(OrderInformation, st.Id);
                           
                        }
                       
                    }
                    UpdateStoreLastRun(st.Id);

                }
            }
        }

        public int AddStore(StoreDto store)
        {
            var context = new orderstatusEntities();
            try
            {
                stores_data storeContext = new stores_data();
                storeContext.name = store.Name;
                storeContext.api_key = store.ApiKey;
                storeContext.url = store.Url;
                storeContext.interval = store.Interval;
                storeContext.last_run = DateTime.Now;
                
                storeContext.dateCreated = DateTime.Now;
                storeContext.dateModified = DateTime.Now;
                storeContext.isActive = 1;
                context.stores_data.AddObject(storeContext);
                context.SaveChanges();
                return storeContext.id;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            finally
            {
                context.Dispose();
            }
        }

        public bool RemoveStore(int id)
        {
            var context = new orderstatusEntities();
            try
            {
                stores_data storeContext = context.stores_data.Where(x => x.id == id).SingleOrDefault();
                if (storeContext != null)
                {
                    storeContext.isActive = 0;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            finally
            {
                context.Dispose();
            }
            return true;
        }

        public StoreDto GetStoreById(int id)
        {
            StoreDto dto = null;
            var context = new orderstatusEntities();
            try
            {
                stores_data storeContext = context.stores_data.Where(x => x.id == id).SingleOrDefault();
                if (storeContext != null)
                {
                    dto = new StoreDto();
                    dto.Id = storeContext.id;
                    dto.Url = storeContext.url;
                    dto.Name = storeContext.name;
                    dto.ApiKey = storeContext.api_key;
                    dto.Interval = int.Parse(storeContext.interval.ToString());
                    
                    dto.CustomShipmentsDtos = GetCustomShipmentByStoreId(storeContext.id);

                }
                return dto;
            }
            catch (Exception ex)
            {
                return dto;
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public void UpdateStoreLastRun(int storeId)
        {
            var context = new orderstatusEntities();
            try
            {
                var store = context.stores_data.Where(x => x.id == storeId).SingleOrDefault();
                store.last_run = DateTime.Now;
                
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
            
        }
        
        public List<OrderStatusDto> GetPublicOrdersStatus()
        {
            orderstatusEntities data = new orderstatusEntities();
            List<OrderStatusDto> listOrdersStatus = new List<OrderStatusDto>();
            try
            {

                var orderStatusList = data.order_status_values.Where(x => x.isPublic == 1).ToList();
                foreach (var orderStatus in orderStatusList)
                {
                    listOrdersStatus.Add(new OrderStatusDto()
                    {
                        Id = orderStatus.id,
                        Text = orderStatus.text
                    });
                }
            }
            catch (InvalidOperationException exc)
            {
                return null;
            }
            catch (ArgumentNullException exc)
            {
                return null;
            }
            catch (NullReferenceException exc)
            {
                return null;
            }
            catch (OptimisticConcurrencyException exc)
            {
                return null;
            }
            catch (UpdateException exc)
            {
                return null;
            }
            finally
            {
                data.Dispose();
            }
            return listOrdersStatus;
        }

        public int AddCustomOrderStatus(string text)
        {
            var context = new orderstatusEntities();
            try
            {
                order_status_values orderst = new order_status_values();
                orderst.text = text;
                orderst.isPublic = 0;
                orderst.isActive = 1;
                orderst.dateCreated = DateTime.Now;
                orderst.dateModified = DateTime.Now;
                context.order_status_values.AddObject(orderst);
                context.SaveChanges();
                return orderst.id;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            finally
            {
                context.Dispose();
            }
        }

        public void UpdateOrderStatusStoreId(int orderStatusId,int storeId)
        {
            var context = new orderstatusEntities();
            try
            {
                //order_status orderst = context..Where(x => x.id == orderStatusId).SingleOrDefault();
                //if (orderst != null)
                //{
                //    orderst.storeid = storeId;
                //    orderst.dateModified = DateTime.Now;
                //    context.SaveChanges();
                //}

            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
        }

        public void CreateCustomShipments(List<string> names, int storeId)
        {
            var context = new orderstatusEntities();
            try
            {
                foreach (var name in names)
                {
                    custom_shipments customShipments = new custom_shipments();
                    customShipments.name = name;
                    customShipments.storeId = storeId;
                    customShipments.dateCreated = DateTime.Now;
                    customShipments.dateModified = DateTime.Now;
                    customShipments.isActive = 1;
                    context.custom_shipments.AddObject(customShipments);
                    context.SaveChanges();
                }


            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
        }

        public List<CustomShipmentsDto> GetCustomShipmentByStoreId(int storeId)
        {

            var context = new orderstatusEntities();
            List<CustomShipmentsDto> list = new List<CustomShipmentsDto>();
            try
            {
                var custShip = context.custom_shipments.Where(x => x.storeId == storeId && x.isActive == 1).ToList();
                foreach (var shipmentse in custShip)
                {
                     list.Add(new CustomShipmentsDto {Id = shipmentse.id, Name = shipmentse.name});
                }


            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
            return list;
        }

        public OrderStatusDto GetAllOrderStatusById(int id)
        {
            var context = new orderstatusEntities();
            OrderStatusDto orderStatusDto = new OrderStatusDto();
            try
            {
                var status = context.order_status_by_store.Where(x=> x.id == id && x.is_valid == 1).SingleOrDefault();
                if (status != null)
                {
                    orderStatusDto = new OrderStatusDto
                                         {
                                             IsPublic = status.order_status_values.isPublic.ToString().Equals("1")?true:false,
                                             Text = status.order_status_values.text
                                         };
                }

            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
            return orderStatusDto;
        }

        public string GetOrderStatusText(int statusId)
        {

            var context = new orderstatusEntities();
            try
            {
                var status = context.order_status_by_store.Where(x => x.id == statusId && x.is_valid == 1).SingleOrDefault();
                if (status != null)
                {
                    return status.order_status_values.text;
                }
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
            return "";
        }

        public bool AddOrderStatusByStoreId(List<string> list,int storeId )
        {
            var context = new orderstatusEntities();
            try
            {
                foreach (var statusId in list)
                {
                    order_status_by_store orderStatusByStore = new order_status_by_store();
                    orderStatusByStore.order_status_value = int.Parse(statusId.ToString());
                    orderStatusByStore.storeid = storeId;
                    orderStatusByStore.date_created = DateTime.Now;
                    orderStatusByStore.date_modified = DateTime.Now;
                    orderStatusByStore.is_valid = 1;
                    context.order_status_by_store.AddObject(orderStatusByStore);
                    context.SaveChanges();
                }
                return true;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                context.Dispose();
            }
        }

        public List<int> GetAllPublicOrderStatusByStore(int storeId)
        {
            var context = new orderstatusEntities();
            List<int> listOfIds = new List<int>();
            try
            {
                var orderStatus = context.order_status_by_store.Where(x => x.storeid == storeId && x.is_valid == 1 && x.order_status_values.isPublic == 1).ToList();
                if (orderStatus.Count > 0)
                {
                    foreach (var status in orderStatus)
                    {
                        listOfIds.Add(status.order_status_value);
                    }
                }

            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
            return listOfIds;
            
        }

        public string  GetNonPublicOrderStatusByStore(int storeId)
        {
            var context = new orderstatusEntities();
            try
            {
                string customStatus = "";
                var orderStatus =
                    context.order_status_by_store.Where(
                        x => x.storeid == storeId && x.is_valid == 1 && x.order_status_values.isPublic == 0).ToList();
                foreach (var status in orderStatus)
                {
                    customStatus += status.order_status_values.text + ";";
                }
                if (customStatus.Length > 0)
                {
                    customStatus = customStatus.Remove(customStatus.Length - 1);
                }
                return customStatus;

            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
            }
            finally
            {
                context.Dispose();
            }
            return "";

        }

        public int EditStore(StoreDto store)
        {
            var context = new orderstatusEntities();
            try
            {
                var storeContext = context.stores_data.Where(x => x.id == store.Id).SingleOrDefault();
                if (storeContext != null)
                {
                    storeContext.name = store.Name;
                    storeContext.api_key = store.ApiKey;
                    storeContext.url = store.Url;
                    storeContext.interval = store.Interval;
                    storeContext.dateModified = DateTime.Now;
                    storeContext.isActive = 1;
                    context.SaveChanges();
                    
                }
                var orderStatus = context.order_status_by_store.Where(x => x.storeid == store.Id).ToList();
                foreach (var orderStatusByStore in orderStatus)
                {
                    context.order_status_by_store.DeleteObject(orderStatusByStore);
                    context.SaveChanges();
                }
                var customShips = context.custom_shipments.Where(x => x.storeId == store.Id).ToList();
                foreach (var customShipmentse in customShips)
                {
                   context.custom_shipments.DeleteObject(customShipmentse);
                    context.SaveChanges();
                }
                return store.Id;


            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return 0;
            }
            finally
            {
                context.Dispose();
            }
        }

        public List<string> GetAllOrderStatusTextByStore(int storeId)
        {
            var context = new orderstatusEntities();
            List<string> ordersStatus = new List<string>();
            try
            {

                var orderStatus = context.order_status_by_store.Where(x => x.storeid == storeId).ToList();
                foreach (var orderStatusByStore in orderStatus)
                {
                    ordersStatus.Add(orderStatusByStore.order_status_values.text);
                }

                return ordersStatus;

            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);

            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
          }
            catch (NullReferenceException exc)
            {
                AccessConnectionHandler.log.Error(exc);

            }
            catch (OptimisticConcurrencyException exc)
            {
                AccessConnectionHandler.log.Error(exc);

            }
            catch (UpdateException exc)
            {
                AccessConnectionHandler.log.Error(exc);

            }
            finally
            {
                context.Dispose();
            }

            return ordersStatus;
        }

        public bool CheckIfOrderExist(string invoiceNumber)
        {
            orderstatusEntities data = new orderstatusEntities();
            try
            {

                var order = data.orders.Where(x => x.invoice_number == invoiceNumber).SingleOrDefault();
                if (order != null)
                {
                    return true;
                }
                
            }
            catch (InvalidOperationException exc)
            {
                return false;
            }
            catch (ArgumentNullException exc)
            {
                return false;
            }
            catch (NullReferenceException exc)
            {
                return false;
            }
            catch (OptimisticConcurrencyException exc)
            {
                return false;
            }
            catch (UpdateException exc)
            {
                return false;
            }
            finally
            {
                data.Dispose();
            }
            return false;

        }

    }
}

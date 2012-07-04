using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using OrderStatusData;
using OrderStatusData.DataTransferObjects;
using OrderStatusData.UPS;
using OrderStatusCore.DataType;
using OrderStatusCore.API_3dCart;

namespace OrderStatusCore
{
    public class Orders
    {
        public bool CreateOrdersFromStore(dynamic data,int storeId)
        {
            var context = new orderstatusEntities();
            try
            {
                foreach (var orderData in data)
                {
                    if (orderData.InvoiceNumber != null && !orderData.InvoiceNumber.ToString().Equals(""))
                    {
                        OrderDto orderDto = new OrderDto()
                                                {
                                                    OrderId = (orderData.OrderID != null)?orderData.OrderID.ToString():"",
                                                    StoreId = storeId,
                                                    InvoiceNumber =
                                                        (orderData.InvoiceNumber != null)
                                                            ? orderData.InvoiceNumber.ToString()
                                                            : "",
                                                    CustomerId =
                                                        (orderData.CustomerID != null)
                                                            ? orderData.CustomerID.ToString()
                                                            : "",
                                                    Email = (orderData.Email != null)
                                                            ? orderData.Email.ToString()
                                                            : "",

                                                };
                        foreach (var ship in orderData.Shippinginfo)
                        {
                            CreateShippingInformation(XDocument.Parse(ship.ToString()),orderDto);
                        }
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
            return true;
        }

        public bool CreateShippingInformation(XDocument xdoc,OrderDto orderDto)
        {
            var context = new orderstatusEntities();
            OrdersRepositoryDbMethods ordersRepositoryDbMethods = new OrdersRepositoryDbMethods();
            try
            {
                int carrierType = 0;
               // <Shipment>
                dynamic shippingInformation = from x in xdoc.Descendants("Shipment")
                    select new
                    {

                        ShipmentID = x.Descendants("ShipmentID").First().Value != string.Empty ?  x.Descendants("ShipmentID").First().Value:"",
                        ShipmentDate = x.Descendants("ShipmentDate").First().Value != string.Empty ?  x.Descendants("ShipmentDate").First().Value:"",
                        Method = x.Descendants("Method").First().Value != string.Empty ?  x.Descendants("Method").First().Value:"",
                        FirstName = x.Descendants("FirstName").First().Value != string.Empty ?  x.Descendants("FirstName").First().Value:"",
                        LastName = x.Descendants("LastName").First().Value != string.Empty ? x.Descendants("LastName").First().Value : "",
                        Company = x.Descendants("Company").First().Value != string.Empty ? x.Descendants("Company").First().Value : "",
                        Address = x.Descendants("Address").First().Value != string.Empty ? x.Descendants("Address").First().Value : "",
                        Address2 = x.Descendants("Address2").First().Value != string.Empty ? x.Descendants("Address2").First().Value : "",
                        City = x.Descendants("City").First().Value != string.Empty ? x.Descendants("City").First().Value : "",
                        ZipCode = x.Descendants("ZipCode").First().Value != string.Empty ? x.Descendants("ZipCode").First().Value : "",
                        StateCode = x.Descendants("StateCode").First().Value != string.Empty ? x.Descendants("StateCode").First().Value : "",
                        CountryCode = x.Descendants("CountryCode").First().Value != string.Empty ? x.Descendants("CountryCode").First().Value : "",
                        Phone = x.Descendants("Phone").First().Value != string.Empty ? x.Descendants("Phone").First().Value : "",
                        Weight = x.Descendants("Weight").First().Value != string.Empty ? x.Descendants("Weight").First().Value : "",
                        Status = x.Descendants("Status").First().Value != string.Empty ? x.Descendants("Status").First().Value : "",
                        InternalComment = x.Descendants("InternalComment").First().Value != string.Empty ? x.Descendants("InternalComment").First().Value : "",
                        TrackingCode = x.Descendants("TrackingCode").First().Value,
                    };
                foreach (var ship in shippingInformation)
                {
                    var firstName = (ship.FirstName != null) ? ship.FirstName.ToString() : "";
                    var lastName = (ship.LastName != null) ? ship.LastName.ToString() : "";
                    orderDto.Name = firstName + " " + lastName;
                    orderDto.Company = (ship.Company != null) ? ship.Company.ToString() : "";
                    orderDto.Address = (ship.Address != null) ? ship.Address.ToString() : "";
                    orderDto.Address2 = (ship.Address2 != null) ? ship.Address2.ToString() : "";
                    orderDto.City = (ship.City != null) ? ship.City.ToString() : "";
                    orderDto.Zip = (ship.ZipCode != null) ? ship.ZipCode.ToString() : "";
                    orderDto.State = (ship.StateCode != null) ? ship.StateCode.ToString() : "";
                    orderDto.Country = (ship.CountryCode != null) ? ship.CountryCode.ToString() : "";
                    orderDto.Phone = (ship.Phone != null) ? ship.Phone.ToString() : "";
                    orderDto.TrackingCode = (ship.TrackingCode != null) ? ship.TrackingCode.ToString() : "";
                    string method = (ship.Method != null) ? ship.Method.ToString() : "";
                    if (!method.Equals(""))
                    {
                        if (method.Contains("UPS"))
                        {
                            orderDto.UpsUspsService = method.Replace("UPS -","");
                            carrierType = CarrierType.Ups;
                        }
                        else
                        {
                            var upsResult = GetAllUpsServices().SingleOrDefault(s => s.Contains(method));
                            if (!string.IsNullOrEmpty(upsResult))
                            {
                                orderDto.UpsUspsService = method.Replace("UPS -", "");
                                carrierType = CarrierType.Ups;
                            }
                            else
                            {
                                if (method.Contains("USPS"))
                                {
                                    orderDto.UpsUspsService = method;
                                    carrierType = CarrierType.Usps;
                                }
                                else
                                {
                                    var uspsResult = GetAllUspsServices().SingleOrDefault(s => s.Contains(method));
                                    if (!string.IsNullOrEmpty(uspsResult))
                                    {

                                        orderDto.UpsUspsService = method;
                                        carrierType = CarrierType.Usps;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        orderDto.UpsUspsService = "Ground";
                    }
                    order newOrder = new order
                                             {
                                                 address = orderDto.Address,
                                                 address2 = orderDto.Address2,
                                                 city = orderDto.City,
                                                 company = orderDto.Company,
                                                 country = orderDto.Country,
                                                 customerid = orderDto.CustomerId,
                                                 date_created = DateTime.Now,
                                                 date_modifed = DateTime.Now,
                                                 email = orderDto.Email,
                                                 email_flag = orderDto.EmailFlag,
                                                 orderId = orderDto.OrderId,
                                                 storeId = orderDto.StoreId,
                                                 invoice_number = orderDto.InvoiceNumber,
                                                 name = orderDto.Name,
                                                 tracking_code = orderDto.TrackingCode,
                                                 phone = orderDto.Phone,
                                                 ups_service = orderDto.UpsUspsService,
                                                 state = orderDto.State
                                             };
                    context.orders.AddObject(newOrder);
                    context.SaveChanges();
                    ordersRepositoryDbMethods.InsertOrderDataToRepository(orderDto);
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
                context.Dispose();
            }
            return true;
        }

        

        public List<OrderDto> GetAllOrdersToProcess()
        {
            List<OrderDto> allOrders = new List<OrderDto>();
            try
            {
                orderstatusEntities data = new orderstatusEntities();
                var orders = data.orders.ToList();
                foreach (var order in orders)
                {
                    OrderDto orderDto = new OrderDto();
                    orderDto.Id = order.id;
                    orderDto.OrderId = order.orderId;
                    allOrders.Add(orderDto);
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

            return allOrders; 
        }

        public List<string> GetAllUpsServices()
        {
            List<string> allServices = new List<string>();
            var context = new orderstatusEntities();
            try
            {
               
                var services = context.ups_services.ToList();
                foreach (var upsService in services)
                {
                    allServices.Add(upsService.service);
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
                context.Dispose();
            }

            return allServices; 
        }

        public List<string> GetAllUspsServices()
        {
            List<string> allServices = new List<string>();
            var context = new orderstatusEntities();
            try
            {

                var services = context.usps_service.ToList();
                foreach (var uspsService in services)
                {
                    allServices.Add(uspsService.service);
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
                context.Dispose();
            }

            return allServices;
        }

        public bool UpdateOrderByInvoice(string invoiceNumber,string trackingNumber)
        {
            
            var context = new orderstatusEntities();
            try
            {

                var order = context.orders.Where(x => x.invoice_number == invoiceNumber).SingleOrDefault();
                if (order != null)
                {
                    var store = context.stores_data.Where(x => x.id == order.storeId).SingleOrDefault();
                    if (store != null)
                    {
                        cartAPI api = new cartAPI();
                        var result = api.updateOrderShipment(store.url, store.api_key,order.invoice_number,"",
                                                     trackingNumber, DateTime.Now.ToShortDateString(), "");
                        if (result.InnerText.Equals("OK"))
                        {
                            var resultStatus = api.updateOrderStatus(store.url, store.api_key, order.invoice_number, "Shipped", "");
                            var asr = resultStatus;
                        }
                    }
                    order.tracking_code = trackingNumber;
                    order.date_modifed = DateTime.Now;

                }

                return true;

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
                context.Dispose();
            }
        }
    }
}

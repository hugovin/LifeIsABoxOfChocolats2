using System.Collections.Generic;
using OrderStatusData.DataTransferObjects;
using OrderStatusData.UPS;
using NLog;
using System;

namespace OrderStatusCore
{
    public class EOD
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();

        public bool OrdersEOD()
        {
            Console.WriteLine("End Of process starts at: " + DateTime.Now);
            OrdersRepositoryDbMethods ordersRepositoryDbMethods = new OrdersRepositoryDbMethods();
            Orders order = new Orders();
            List<OrderDto> listOfOrders = ordersRepositoryDbMethods.ReadAllEodOrderDataToRepository();
            foreach (var listOfOrder in listOfOrders)
            {
                log.Info("//--------------------------------------------------------------------------------------------");
                log.Info("End of day for invoice starts: " + listOfOrder.InvoiceNumber+" at: "+ DateTime.Now.ToString());
                if (!string.IsNullOrEmpty(listOfOrder.InvoiceNumber))
                {
                    try
                    {
                        order.UpdateOrderByInvoice(listOfOrder.InvoiceNumber, listOfOrder.TrackingCode);
                        ordersRepositoryDbMethods.RemoveOrderFromOrderDataToRepository(listOfOrder.InvoiceNumber);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error processing: "+ listOfOrder.InvoiceNumber+" ,Error: "+ex.Message);
                    }
                    //ordersRepositoryDbMethods.RemoveOrderFromMyEODShipmentRepository(listOfOrder.InvoiceNumber);*/
                }
                log.Info("End of day for invoice ends: " + listOfOrder.InvoiceNumber + " at: " + DateTime.Now.ToString());

            }
            Console.WriteLine("End Of process ends at: " + DateTime.Now);
            return ordersRepositoryDbMethods.CleanOrderDataToRepository();
        }
    }
}

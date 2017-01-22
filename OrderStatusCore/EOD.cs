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
            OrdersRepositoryDbMethods ordersRepositoryDbMethods = new OrdersRepositoryDbMethods();
            Orders order = new Orders();
            List<OrderDto> listOfOrders = ordersRepositoryDbMethods.ReadAllEodOrderDataToRepository();
            foreach (var listOfOrder in listOfOrders)
            {
                log.Info("//--------------------------------------------------------------------------------------------");
                log.Info("End of day for invoice starts: " + listOfOrder.InvoiceNumber+" at: "+ DateTime.Now.ToString());
                if (!string.IsNullOrEmpty(listOfOrder.InvoiceNumber))
                {
                    order.UpdateOrderByInvoice(listOfOrder.InvoiceNumber, listOfOrder.TrackingCode);
                    ordersRepositoryDbMethods.RemoveOrderFromOrderDataToRepository(listOfOrder.InvoiceNumber);
                    //ordersRepositoryDbMethods.RemoveOrderFromMyEODShipmentRepository(listOfOrder.InvoiceNumber);*/
                }
                log.Info("End of day for invoice ends: " + listOfOrder.InvoiceNumber + " at: " + DateTime.Now.ToString());

            }
            return ordersRepositoryDbMethods.CleanOrderDataToRepository();
        }
    }
}

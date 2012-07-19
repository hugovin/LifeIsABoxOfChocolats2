using System.Collections.Generic;
using OrderStatusData.DataTransferObjects;
using OrderStatusData.UPS;

namespace OrderStatusCore
{
    public class EOD
    {
        public bool OrdersEOD()
        {
            OrdersRepositoryDbMethods ordersRepositoryDbMethods = new OrdersRepositoryDbMethods();
            //Orders order = new Orders();
            //List<OrderDto> listOfOrders = ordersRepositoryDbMethods.ReadAllEodOrderDataToRepository();
            //foreach (var listOfOrder in listOfOrders)
            //{
            //    if (!string.IsNullOrEmpty(listOfOrder.InvoiceNumber))
            //    {
            //        order.UpdateOrderByInvoice(listOfOrder.InvoiceNumber, listOfOrder.TrackingCode);
            //        ordersRepositoryDbMethods.RemoveOrderFromOrderDataToRepository(listOfOrder.InvoiceNumber);
            //    }

            //}
            return ordersRepositoryDbMethods.CleanOrderDataToRepository();
        }
    }
}

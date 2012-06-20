using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderStatusData.DataTransferObjects;
using OrderStatusData.UPS;
using OrderStatusData.USPS;

namespace OrderStatusCore
{
    public class EOD
    {
        public bool UpsEOD()
        {
            UpsDbMethods upsDbMethods = new UpsDbMethods();
            Orders order = new Orders();
            List<OrderDto> listOfOrders = upsDbMethods.ReadAllEodUpsData();
            foreach (var listOfOrder in listOfOrders)
            {
                order.UpdateOrderByInvoice(listOfOrder.InvoiceNumber, listOfOrder.TrackingCode);
                upsDbMethods.RemoveOrderFromUpsImport(listOfOrder.InvoiceNumber);
            }
            return upsDbMethods.CleanUpsAccessFile();
        }

        public bool UspsEOD()
        {
            UspsDbMethods uspsDbMethods = new UspsDbMethods();
            Orders order = new Orders();
            List<OrderDto> listOfOrders = uspsDbMethods.ReadAllEodUspsData();
            foreach (var listOfOrder in listOfOrders)
            {
                order.UpdateOrderByInvoice(listOfOrder.InvoiceNumber, listOfOrder.TrackingCode);
                uspsDbMethods.RemoveOrderFromUspsExport(listOfOrder.InvoiceNumber);
            }
            return uspsDbMethods.CleanUspsAccessFile();

        }
    }
}

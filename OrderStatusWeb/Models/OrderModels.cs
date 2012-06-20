using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderStatusData.DataTransferObjects;

namespace OrderStatusWeb.Models
{
    public class OrderModels
    {
        public class AllOrders
        {
            public List<OrderDto> Orders;

            public AllOrders()
            {
                Orders = new List<OrderDto>();
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using OrderStatusCore;



namespace OrderStatus
{
    public class OrderStatusCheck
    {

        public void CheckOrders()
        {
            Stores stores = new Stores();
            stores.CheckAllOrders();
        }
    }
}

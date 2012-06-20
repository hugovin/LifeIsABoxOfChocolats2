using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;


namespace OrderStatus
{
    public partial class OrderStatusService : ServiceBase
    {
        public OrderStatusService()
        {
            InitializeComponent();
            
            Logs.Source = "MySource";
            Logs.Log = "MyNewLog";

        }

        //Debug Mode
        public void Start()
        {
            OrderStatusCheck orders = new OrderStatusCheck();
            orders.CheckOrders();
        }

        protected override void OnStart(string[] args)
        {
            Logs.WriteEntry("Started at:" + DateTime.UtcNow.ToString());
        }

        protected override void OnStop()
        {
            Logs.WriteEntry("Stopped at:" + DateTime.UtcNow.ToString());
        }

    }
}

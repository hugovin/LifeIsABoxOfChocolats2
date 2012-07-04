using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
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
            Logs.WriteEntry("Order status Started at:" + DateTime.UtcNow.ToString());
            OrderStatusCheck orders = new OrderStatusCheck();
            orders.CheckOrders();
            
        }

        protected override void OnStop()
        {
            Logs.WriteEntry("Order status Stopped at:" + DateTime.UtcNow.ToString());
        }

        private void Logs_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

    }
}

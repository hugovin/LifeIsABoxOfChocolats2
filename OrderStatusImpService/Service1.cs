using System;
using System.ServiceProcess;
using System.Threading;
using OrderStatusCore;


namespace OrderStatusImpService
{
    public partial class Service1 : ServiceBase
    {
        private Stores stores = new Stores();
        public Service1()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("OrderStatusImport"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "OrderStatusImport", "OrderStatusLog");
            }
            eventLog1.Source = "OrderStatusImport";
            eventLog1.Log = "OrderStatusLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Service Starts at: "+ DateTime.Now);
            var worker = new Thread(RunProcess);
            worker.Name = "PullDataThread";
            worker.IsBackground = true;
            worker.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service Stops at: " + DateTime.Now);
        }

        protected override void OnContinue()
        {
            RunProcess();
        }

        void RunProcess()
        {
            while (true)
            {

                    eventLog1.WriteEntry("Data Pull starts at: " + DateTime.Now);
                    stores.CheckAllOrders();
                    eventLog1.WriteEntry("Data Pull stops at: " + DateTime.Now);
                    if (DateTime.Now >= DateTime.Parse("6:00 pm") && DateTime.Now <= DateTime.Parse("7:00 pm"))
                    {
                        eventLog1.WriteEntry("EOD Process Starts at: " + DateTime.Now);
                        EOD eod = new EOD();
                        eod.OrdersEOD();
                        eventLog1.WriteEntry("EOD Process Ends at: " + DateTime.Now);
                    }
                    Thread.Sleep(3600000);
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }
    }
}

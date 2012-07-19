using System;
using System.ServiceProcess;
using System.Threading;
using OrderStatusCore;

namespace OrderStatusEOD
{
    public partial class EODwinService : ServiceBase
    {
        public EODwinService()
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
            eventLog1.WriteEntry("EOD Service Starts at: " + DateTime.Now);
            var worker = new Thread(RunProcess);
            worker.Name = "EODThread";
            worker.IsBackground = true;
            worker.Start();
            
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("EOD Service Stops at: " + DateTime.Now);
        }

        protected override void OnContinue()
        {
            RunProcess();
        }

        void RunProcess()
        {
            eventLog1.WriteEntry("Entro A Correr EOD " + DateTime.Now);
            while (true)
            {
                if (DateTime.Now == DateTime.Parse("03:33:00 pm"))
                {
                    eventLog1.WriteEntry("EOD Process Starts at: " + DateTime.Now);
                    EOD eod = new EOD();
                    eod.OrdersEOD();
                    eventLog1.WriteEntry("EOD Process Ends at: " + DateTime.Now);

                }
            }
            
        }
    }
}

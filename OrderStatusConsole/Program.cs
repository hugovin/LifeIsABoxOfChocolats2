using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OrderStatusCore;
using NLog;
using System.Threading;
using System.Configuration;

namespace OrderStatusConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Stores stores = new Stores();
            var filePath = ConfigurationManager.AppSettings["logPathFile"];
            Logger log = LogManager.GetCurrentClassLogger();
            try
            {
                Console.WriteLine("Console Start successfully");
                while (true)
                {
                    try
                    {
                        if (!File.Exists(filePath))
                        {
                           File.Create(filePath).Dispose();
                        }
                        Console.WriteLine("Data Pull starts at: " + DateTime.Now);
                        using (StreamWriter writer =  File.AppendText(filePath))
                        {
                            writer.WriteLine("Data Pull starts at: " + DateTime.Now);
                        }
                        stores.CheckAllOrders();
                        Console.WriteLine("Data Pull stops at: " + DateTime.Now);
                        using (StreamWriter writer =  File.AppendText(filePath))
                        {
                            writer.WriteLine("Data Pull stops at: " + DateTime.Now);
                        }
                        if (DateTime.Now >= DateTime.Parse("6:00 pm") && DateTime.Now <= DateTime.Parse("7:00 pm"))
                        { 
                            Console.WriteLine("EOD Process Starts at: " + DateTime.Now);
                            using (StreamWriter writer =  File.AppendText(filePath))
                            {
                                writer.WriteLine("EOD Process Starts at: " + DateTime.Now);
                            }
                            EOD eod = new EOD();
                        eod.OrdersEOD();
                            using (StreamWriter writer =  File.AppendText(filePath))
                            {
                                writer.WriteLine("EOD Process Ends at: " + DateTime.Now);
                            }
                            Console.WriteLine("EOD Process Ends at: " + DateTime.Now);
                       }
                        Console.WriteLine("Console sleeps at: " + DateTime.Now);
                        Thread.Sleep(3600000);
                        Console.WriteLine("Console awake at: " + DateTime.Now);
                    }
                    catch (Exception ex) {
                        Console.WriteLine("Console Error on program" + ex.Message + " at: " + DateTime.Now);
                        Thread.Sleep(3600000);
                    }
                    
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Something went very Wrong Error:" + ex.Message + " at: " + DateTime.Now);
            }

        }
    }
}

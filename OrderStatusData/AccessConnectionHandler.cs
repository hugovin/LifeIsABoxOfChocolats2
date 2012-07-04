using System;
using System.Data.OleDb;
using System.Configuration;
using NLog;


namespace OrderStatusData
{
    public class AccessConnectionHandler
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();
        public  OleDbConnection GetOrdersRepositoryConnection()
        {
            try
            {
                return new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessOrdersRepository"].ConnectionString);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

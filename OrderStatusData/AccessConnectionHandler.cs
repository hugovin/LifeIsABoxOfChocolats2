using System;
using System.Data.OleDb;
using System.Configuration;
using NLog;


namespace OrderStatusData
{
    public class AccessConnectionHandler
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();
        public  OleDbConnection GetUpsConnection()
        {
            try
            {
                return new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessUps"].ConnectionString);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public OleDbConnection GetUspsConnection()
        {
            try
            {
                return new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessUsps"].ConnectionString);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public OleDbConnection GetExpediaConnection()
        {
            try
            {
                return new OleDbConnection(ConfigurationManager.ConnectionStrings["AccessUps"].ConnectionString);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using OrderStatusData.DataTransferObjects;


namespace OrderStatusData.UPS
{
    public class OrdersRepositoryDbMethods
    {
        AccessConnectionHandler connection = new AccessConnectionHandler();
        public bool InsertOrderDataToRepository( OrderDto orderDto)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                
                conn  = connection.GetOrdersRepositoryConnection();
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                cmd.Connection = conn;
                
                cmd.CommandText =
                    "INSERT INTO OrdersImport([orderid],[customerid],[name],[company],[address],[address2]," +
                    "[city],[state],[zip],[country],[phone],[email],[carrier_service]) " +
                    "VALUES (@Orderid, @Customerid,@Name,@Company,@Address,@Address2," +
                    "@City,@State,@Zip,@Country,@Phone,@Email,@Service)";

                cmd.Parameters.Add("@Orderid", OleDbType.VarChar).Value = orderDto.InvoiceNumber;
                cmd.Parameters.Add("@Customerid", OleDbType.VarChar).Value = orderDto.CustomerId;
                cmd.Parameters.Add("@Name", OleDbType.VarChar).Value = orderDto.Name;
                cmd.Parameters.Add("@Company", OleDbType.VarChar).Value = orderDto.Company;
                cmd.Parameters.Add("@Address", OleDbType.VarChar).Value = orderDto.Address;
                cmd.Parameters.Add("@Address2", OleDbType.VarChar).Value = orderDto.Address2;
                cmd.Parameters.Add("@City", OleDbType.VarChar).Value = orderDto.City;
                cmd.Parameters.Add("@State", OleDbType.VarChar).Value = orderDto.State;
                cmd.Parameters.Add("@Zip", OleDbType.VarChar).Value = (orderDto.Zip) ?? orderDto.Zip;
                cmd.Parameters.Add("@Country", OleDbType.VarChar).Value = orderDto.Country;
                cmd.Parameters.Add("@Phone", OleDbType.VarChar).Value = orderDto.Phone;
                cmd.Parameters.Add("@Email", OleDbType.VarChar).Value = orderDto.Email;
                cmd.Parameters.Add("@Service", OleDbType.VarChar).Value = orderDto.UpsUspsService;
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool ReadAllOrderDataToRepository()
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {

                conn = connection.GetOrdersRepositoryConnection();

                OleDbCommand Command = conn.CreateCommand();
                // create the DataSet
                DataSet ds = new DataSet();
                // open the connection
                conn.Open();
                // run the query
                Command.CommandText = "Select * From OrdersImport";
                OleDbDataAdapter adapter = new OleDbDataAdapter(Command);
                adapter.Fill(ds);


                conn.Close();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<OrderDto> ReadAllEodOrderDataToRepository()
        {
            OleDbConnection conn = new OleDbConnection();
            List<OrderDto> listOfOrders = new List<OrderDto>();
            try
            {
                
                conn = connection.GetOrdersRepositoryConnection();
                conn.Open();


                OleDbCommand cmd = conn.CreateCommand();
                // 'DEFAULT' is used
                cmd.CommandText = "Select * From MyEODShipment";

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(1) != null && !String.IsNullOrEmpty(reader.GetString(1)))
                            {
                                OrderDto dto = new OrderDto
                                {
                                    InvoiceNumber = reader.GetString(1),
                                    TrackingCode = reader.GetString(2)
                                };
                                listOfOrders.Add(dto);
                            }

                        }
                        catch (Exception)
                        {
                           //
                        }
                       

                    }
                    reader.Close();
                }

                
                conn.Close();
                return listOfOrders;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return listOfOrders;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return listOfOrders;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return listOfOrders;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool CleanOrderDataToRepository()
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                conn = connection.GetOrdersRepositoryConnection();
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO ConsolidateEOD (OrderId, TrackingNumber,Weight,TotalCharges,TotalShippedCharges) SELECT * FROM MyEODShipment";
                cmd.ExecuteNonQuery();

                conn.Close();
                return DeleteOrderDataToRepository();
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool DeleteOrderDataToRepository()
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                conn = connection.GetOrdersRepositoryConnection();
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM MyEODShipment";
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool RemoveOrderFromOrderDataToRepository(string invoice)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {

                conn = connection.GetOrdersRepositoryConnection();
                OleDbCommand cmd = new OleDbCommand();
                conn.Open();
                cmd.Connection = conn;
              
                cmd.CommandText =
                    "DELETE FROM OrdersImport WHERE Orderid = @id";

                cmd.Parameters.Add("@id", OleDbType.VarChar).Value = invoice;
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (ArgumentNullException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            catch (OleDbException exc)
            {
                AccessConnectionHandler.log.Error(exc);
                return false;
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}

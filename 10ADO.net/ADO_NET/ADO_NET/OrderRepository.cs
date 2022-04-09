using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_NET
{
    class OrderRepository : IOrderRepository
    {
        private readonly string ConnectionString;  

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> Orders = new List<Order>();

           using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "SELECT * FROM Orders";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    Order order = new Order();


                    var t = reader.GetValue(3);

                    order.CustomerID = DBNull.Value.Equals(reader.GetValue(1)) ? null : (string)reader.GetValue(1);
                    order.EmployeeID = (int)reader.GetValue(2);
                    order.OrderID = (int)reader.GetValue(0);

                    if (!DBNull.Value.Equals(reader.GetValue(3)))
                    { order.OrderDate = (DateTime)reader.GetValue(3);}
                    else { order.OrderDate = null; }
                    
                    if (!DBNull.Value.Equals(reader.GetValue(4)))
                    { order.RequiredDate = (DateTime)reader.GetValue(4); }
                    else { order.RequiredDate = null; }

                    if (!DBNull.Value.Equals(reader.GetValue(5)))
                    { order.ShippedDate = (DateTime)reader.GetValue(5); }
                    else { order.ShippedDate = null; }

                    order.ShipVia = (int)reader.GetValue(6);
                    order.ShipName = DBNull.Value.Equals(reader.GetValue(8)) ? null : (string)reader.GetValue(8);
                    order.ShipAddress = DBNull.Value.Equals(reader.GetValue(9)) ? null : (string)reader.GetValue(9);
                    order.ShipCity = DBNull.Value.Equals(reader.GetValue(10)) ? null : (string)reader.GetValue(10);
                    order.ShipCountry = DBNull.Value.Equals(reader.GetValue(13)) ? null : (string)reader.GetValue(13);
                    order.ShipRegion = DBNull.Value.Equals(reader.GetValue(11)) ? null : (string)reader.GetValue(11);

                    Orders.Add(order);
                }
            }
            return Orders;
        }
    }
}

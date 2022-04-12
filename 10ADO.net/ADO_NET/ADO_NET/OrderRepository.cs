using ADO_NET.Model;
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


        public void CreateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"INSERT INTO Orders (OrderDate, ShipCity) VALUES (GETDATE(), 'Москва')";
                command.Connection = connection;
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }
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

                    order.ShipName = DBNull.Value.Equals(reader.GetValue(8)) ? null : (string)reader.GetValue(8);
                    order.ShipAddress = DBNull.Value.Equals(reader.GetValue(9)) ? null : (string)reader.GetValue(9);
                    order.ShipCity = DBNull.Value.Equals(reader.GetValue(10)) ? null : (string)reader.GetValue(10);
                    order.ShipCountry = DBNull.Value.Equals(reader.GetValue(13)) ? null : (string)reader.GetValue(13);
                    order.ShipRegion = DBNull.Value.Equals(reader.GetValue(11)) ? null : (string)reader.GetValue(11);

                    if (order.OrderDate == null)
                    {
                        order.OrderStatus = Order.Status.New;
                    }
                    else
                    {
                        if (order.ShippedDate == null)
                        {
                            order.OrderStatus = Order.Status.AtWork;
                        }
                        else
                        {
                            order.OrderStatus = Order.Status.Completed;
                        }      
                    }
                    Orders.Add(order);
                }
            }
            return Orders;
        }

        public Order GetOrder(int id)
        {
            Order order = new Order();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"SELECT * FROM Orders WHERE OrderID = {id}";

                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                if (reader.HasRows == false)
                    throw new Exception("В таблице Orders из базы данных, нет данных");

                order.OrderID = (int)reader.GetValue(0);

                Console.WriteLine(reader.GetValue(3));
                Console.WriteLine(reader.GetValue(3).GetType());

                if (!DBNull.Value.Equals(reader.GetValue(3)))
                { order.OrderDate = (DateTime)reader.GetValue(3); }
                else { order.OrderDate = null; }

                

                if (!DBNull.Value.Equals(reader.GetValue(4)))
                { order.RequiredDate = (DateTime)reader.GetValue(4); }
                else { order.RequiredDate = null; }

                if (!DBNull.Value.Equals(reader.GetValue(5)))
                { order.ShippedDate = (DateTime)reader.GetValue(5); }
                else { order.ShippedDate = null; }

                order.ShipName = DBNull.Value.Equals(reader.GetValue(8)) ? null : (string)reader.GetValue(8);
                order.ShipAddress = DBNull.Value.Equals(reader.GetValue(9)) ? null : (string)reader.GetValue(9);
                order.ShipCity = DBNull.Value.Equals(reader.GetValue(10)) ? null : (string)reader.GetValue(10);
                order.ShipCountry = DBNull.Value.Equals(reader.GetValue(13)) ? null : (string)reader.GetValue(13);
                order.ShipRegion = DBNull.Value.Equals(reader.GetValue(11)) ? null : (string)reader.GetValue(11);

                if (order.OrderDate == null)
                {
                    order.OrderStatus = Order.Status.New;
                }
                else
                {
                    if (order.ShippedDate == null)
                    {
                        order.OrderStatus = Order.Status.AtWork;
                    }
                    else
                    {
                        order.OrderStatus = Order.Status.Completed;
                    }
                }
                reader.Close();

                SqlCommand command1 = new SqlCommand();

                command1.CommandText = $"SELECT * FROM [Order Details] AS OD JOIN Products ON OD.ProductID = Products.ProductID WHERE OD.OrderID = {id}";
                command1.Connection = connection;

                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.HasRows == false)
                    throw new Exception("В таблице Order Details из базы данных, нет данных");

                while (reader1.Read())
                {
                    OrderDetails orderDetails = new OrderDetails(new Product());
                    
                    orderDetails.Product.ProductId = (int)reader1.GetValue(1);
                    orderDetails.Quantity = (Int16)reader1.GetValue(3);
                    orderDetails.Product.ProductName = DBNull.Value.Equals(reader1.GetValue(6)) ? null : (string)reader1.GetValue(6);
                    orderDetails.Product.QuantityPerUnit = DBNull.Value.Equals(reader1.GetValue(9)) ? null : (string)reader1.GetValue(9);

                    order.orderDetails.Add(orderDetails);
                }
            }
             return order;
        }
    }
}

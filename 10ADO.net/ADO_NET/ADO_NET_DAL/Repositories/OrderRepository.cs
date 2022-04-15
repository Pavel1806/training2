using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using ADO_NET_ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_NET_DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string ConnectionString;

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }


        public void Create(ViewOrder viewOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                Dictionary<int, int> productIdQuantity = new Dictionary<int, int>();

                int orderId = 0;

                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();

                    command.CommandText = $"INSERT INTO Orders (OrderDate, ShipCity) VALUES (GETDATE(), 'Москва'); SELECT SCOPE_IDENTITY() as [SCOPE_IDENTITY]";

                    command.Connection = connection;

                    decimal number = (decimal)command.ExecuteScalar();

                    orderId = (int)number;

                    

                    foreach (var item in viewOrder.orderDetails)
                    {
                        IProductRepository repository = new ProductRepository(ConnectionString);

                        Product product = repository.GetById(item.ProductId);

                        OrderDetails orderDetails = new OrderDetails();

                        var unitPrice = product.UnitPrice;

                        if (item.Quantity > product.UnitsInStock)
                            throw new Exception("На складе продукта не хватает");

                        productIdQuantity[product.ProductId] = item.Quantity;

                        repository.DecreaseUnitsInStock(product.ProductId, item.Quantity);

                        SqlCommand command1 = new SqlCommand();

                        command1.CommandText = $"INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity) VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity)";

                        command1.Connection = connection;

                        SqlParameter OrderIdParam = new SqlParameter("@OrderId", orderId);
                        command1.Parameters.Add(OrderIdParam);

                        SqlParameter ProductIdParam = new SqlParameter("@ProductId", item.ProductId);
                        command1.Parameters.Add(ProductIdParam);

                        SqlParameter UnitPriceParam = new SqlParameter("@UnitPrice", unitPrice);
                        command1.Parameters.Add(UnitPriceParam);

                        SqlParameter QuantityParam = new SqlParameter("@Quantity", item.Quantity);
                        command1.Parameters.Add(QuantityParam);

                        int p = command1.ExecuteNonQuery();
                    }
                }
                catch
                {
                    Delete(orderId);

                    foreach(var item in productIdQuantity)
                    {
                        IProductRepository repository = new ProductRepository(ConnectionString);

                        repository.IncreaseUnitsInStock(item.Key, item.Value);
                    }
                }

            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"DELETE [Order Details] WHERE OrderID = {id} DELETE Orders WHERE OrderID = {id}";

                command.Connection = connection;

                int p = command.ExecuteNonQuery();

                return p;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> Orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                    Orders.Add(order);
                }
            }
            return Orders;
        }

        public Order GetById(int id)
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
                    return null;
                    //throw new Exception("В таблице Orders из базы данных, нет данных");

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

                command1.CommandText = $"SELECT OD.ProductID, OrderID, OD.UnitPrice, Quantity, ProductName, QuantityPerUnit, UnitsInStock, Products.UnitPrice " +
                    $"FROM [Order Details] AS OD " +
                    $"JOIN Products ON OD.ProductID = Products.ProductID WHERE OD.OrderID = {id}";
                command1.Connection = connection;

                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.HasRows == false)
                    throw new Exception("В таблице Order Details из базы данных, нет данных");

                while (reader1.Read())
                {
                    OrderDetails orderDetails = new OrderDetails(new Product());
                    //Product product = new Product();

                    orderDetails.UnitPrice = (decimal)reader1.GetValue(2);
                    orderDetails.ProductId = (int)reader1.GetValue(0);
                    orderDetails.Product.ProductId = orderDetails.ProductId;
                    orderDetails.Quantity = (Int16)reader1.GetValue(3);
                    orderDetails.Product.ProductName = DBNull.Value.Equals(reader1.GetValue(4)) ? null : (string)reader1.GetValue(4);
                    orderDetails.Product.QuantityPerUnit = DBNull.Value.Equals(reader1.GetValue(5)) ? null : (string)reader1.GetValue(5);
                    orderDetails.Product.UnitsInStock = (Int16)reader1.GetValue(6);
                    orderDetails.Product.UnitPrice = (decimal)reader1.GetValue(7);

                    order.orderDetails.Add(orderDetails);
                }
            }
            return order;
        }

        public void Update(ViewOrder orderNew)
        {
            Order orderOld = GetById(orderNew.OrderID);

            if (orderOld.OrderStatus != Order.Status.New)
                throw new Exception("Заказ менять нельзя");

            foreach (var Old in orderOld.orderDetails)
            {
                foreach (var New in orderNew.orderDetails)
                {
                    if (Old.ProductId == New.ProductId)
                    {
                        if (Old.Quantity - New.Quantity == 0)
                        {
                              
                        }
                        else if (Old.Quantity - New.Quantity < 0)
                        {

                        }
                        else
                        {

                        }
                        orderNew.orderDetails.Remove(New);
                        break; 
                    }

                }

                foreach (var New in orderNew.orderDetails)
                {

                    //orderOld.orderDetails.Add();


                }
            }
        }
    }
}

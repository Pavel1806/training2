using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using ADO_NET_ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ADO_NET_DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string ConnectionString;

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Метод создания Order
        /// </summary>
        /// <param name="viewOrder">Order который приходит из интерфейса</param>
        public int Create(ViewOrder viewOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                Dictionary<int, int> productIdQuantity = new Dictionary<int, int>();

                int orderId = 0;

                try
                {
                    connection.Open();

                    string request = $"INSERT INTO Orders (OrderDate, ShipCity) VALUES (GETDATE(), 'Москва'); SELECT SCOPE_IDENTITY() as [SCOPE_IDENTITY]";

                    SqlCommand command = new SqlCommand(request, connection);

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

                        string request1 = $"INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity) VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity)";

                        SqlCommand command1 = new SqlCommand(request1, connection);

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

                    foreach (var item in productIdQuantity)
                    {
                        IProductRepository repository = new ProductRepository(ConnectionString);

                        repository.IncreaseUnitsInStock(item.Key, item.Value);
                    }
                }

                return orderId;
            }
        }

        /// <summary>
        /// Метод удаления Order
        /// </summary>
        /// <param name="id">Идентификатор Order</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var order = GetById(id);

            if (order.OrderStatus == Order.Status.Completed)
                return 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = $"DELETE [Order Details] WHERE OrderID = {id} DELETE Orders WHERE OrderID = {id}";

                SqlCommand command = new SqlCommand(request, connection);

                int p = command.ExecuteNonQuery();

                return p;
            }
        }

        /// <summary>
        /// Получение всех Order
        /// </summary>
        /// <returns>Все Order</returns>
        public IEnumerable<Order> GetAll()
        {
            List<Order> Orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = "SELECT * FROM Orders";

                SqlCommand command = new SqlCommand(request, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order();

                    order.OrderID = (int)reader.GetValue(0);
                    order.OrderDate = DBNull.Value.Equals(reader.GetValue(3)) ? null : (DateTime?)reader.GetValue(3);
                    order.RequiredDate = DBNull.Value.Equals(reader.GetValue(4)) ? null : (DateTime?)reader.GetValue(4);
                    order.ShippedDate = DBNull.Value.Equals(reader.GetValue(5)) ? null : (DateTime?)reader.GetValue(5);
                    order.ShipName = DBNull.Value.Equals(reader.GetValue(8)) ? null : (string)reader.GetValue(8);
                    order.ShipAddress = DBNull.Value.Equals(reader.GetValue(9)) ? null : (string)reader.GetValue(9);
                    order.ShipCity = DBNull.Value.Equals(reader.GetValue(10)) ? null : (string)reader.GetValue(10);
                    order.ShipCountry = DBNull.Value.Equals(reader.GetValue(13)) ? null : (string)reader.GetValue(13);
                    order.ShipRegion = DBNull.Value.Equals(reader.GetValue(11)) ? null : (string)reader.GetValue(11);

                    if (order.OrderDate == null)
                    {order.OrderStatus = Order.Status.New;}
                    else
                    {if (order.ShippedDate == null)
                        {order.OrderStatus = Order.Status.AtWork;}
                        else
                        { order.OrderStatus = Order.Status.Completed;}}

                    Orders.Add(order);
                }
            }
            return Orders;
        }

        /// <summary>
        /// Получение Order по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Order</param>
        /// <returns></returns>
        public Order GetById(int id)
        {

            Order order = new Order();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = $"SELECT * FROM Orders WHERE OrderID = {id}";

                SqlCommand command = new SqlCommand(request, connection);

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                if (reader.HasRows == false)
                    return null;
                //throw new Exception("В таблице Orders из базы данных, нет данных");

                order.OrderID = (int)reader.GetValue(0);

                order.OrderDate = DBNull.Value.Equals(reader.GetValue(3)) ? null : (DateTime?)reader.GetValue(3);
                order.RequiredDate = DBNull.Value.Equals(reader.GetValue(4)) ? null : (DateTime?)reader.GetValue(4);
                order.ShippedDate = DBNull.Value.Equals(reader.GetValue(5)) ? null : (DateTime?)reader.GetValue(5);
                order.ShipName = DBNull.Value.Equals(reader.GetValue(8)) ? null : (string)reader.GetValue(8);
                order.ShipAddress = DBNull.Value.Equals(reader.GetValue(9)) ? null : (string)reader.GetValue(9);
                order.ShipCity = DBNull.Value.Equals(reader.GetValue(10)) ? null : (string)reader.GetValue(10);
                order.ShipCountry = DBNull.Value.Equals(reader.GetValue(13)) ? null : (string)reader.GetValue(13);
                order.ShipRegion = DBNull.Value.Equals(reader.GetValue(11)) ? null : (string)reader.GetValue(11);

                if (order.OrderDate == null)
                { order.OrderStatus = Order.Status.New; }
                else
                {
                    if (order.ShippedDate == null)
                    { order.OrderStatus = Order.Status.AtWork; }
                    else
                    { order.OrderStatus = Order.Status.Completed; }
                }
                reader.Close();

                string request1 = $"SELECT OD.ProductID, OrderID, OD.UnitPrice, Quantity, ProductName, QuantityPerUnit, UnitsInStock, Products.UnitPrice " +
                    $"FROM [Order Details] AS OD " +
                    $"JOIN Products ON OD.ProductID = Products.ProductID WHERE OD.OrderID = {id}";

                SqlCommand command1 = new SqlCommand(request1, connection);

                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.HasRows == false)
                    throw new Exception("В таблице Order Details из базы данных, нет данных");

                while (reader1.Read())
                {
                    OrderDetails orderDetails = new OrderDetails(new Product());

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

        /// <summary>
        /// Изменение Order
        /// </summary>
        /// <param name="viewOrder">Order который приходит из интерфейса</param>
        public void Update(ViewOrder viewOrder)
        {
            Order orderOld = GetById(viewOrder.OrderID);

            //if (orderOld.OrderStatus != Order.Status.New)
            //    throw new Exception("Заказ менять нельзя");

            Order orderNew = new Order();

            orderNew.OrderID = orderOld.OrderID;

            foreach (var Old in orderOld.orderDetails)
            {
                foreach (var New in viewOrder.orderDetails)
                {
                    if (Old.ProductId == New.ProductId)
                    {
                        orderNew.orderDetails.Add(new OrderDetails()
                        {
                            ProductId = New.ProductId,
                            OrderId = Old.OrderId,
                            Quantity = New.Quantity,
                            UnitPrice = Old.UnitPrice
                        });

                        viewOrder.orderDetails.Remove(New);

                        break;
                    }
                }
            }

            ProductRepository repository = new ProductRepository(ConnectionString);

            List<Product> products = new List<Product>();

            foreach (var New in viewOrder.orderDetails)
            {
                Product product = repository.GetById(New.ProductId);

                products.Add(product);

                orderNew.orderDetails.Add(new OrderDetails()
                {
                    ProductId = New.ProductId,
                    OrderId = orderOld.OrderID,
                    Quantity = New.Quantity,
                    UnitPrice = product.UnitPrice
                });
            }

            OrderRepository orderRepository = new OrderRepository(ConnectionString);

            orderRepository.DeleteOrderDetails(orderOld);

            orderRepository.CreateOrderDetails(orderNew);
        }

        /// <summary>
        /// Удаление OrderDetails
        /// </summary>
        /// <param name="order">Order у которого надо удалить OrderDetails</param>
        /// <returns></returns>
        private int DeleteOrderDetails(Order order)
        {
            IProductRepository repository = new ProductRepository(ConnectionString);

            foreach (var item in order.orderDetails)
            {
                Product product = repository.GetById(item.ProductId);

                repository.IncreaseUnitsInStock(product.ProductId, item.Quantity);
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = $"DELETE [Order Details] WHERE OrderID = {order.OrderID}";

                SqlCommand command = new SqlCommand(request, connection);

                int p = command.ExecuteNonQuery();

                return p;
            }
        }

        /// <summary>
        /// Создание OrderDetails
        /// </summary>
        /// <param name="order">Order в котором надо создать OrderDetails</param>
        /// <returns></returns>
        private int CreateOrderDetails(Order order)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                Dictionary<int, int> productIdQuantity = new Dictionary<int, int>();

                foreach (var item in order.orderDetails)
                {
                    IProductRepository repository = new ProductRepository(ConnectionString);

                    Product product = repository.GetById(item.ProductId);

                    OrderDetails orderDetails = new OrderDetails();

                    var unitPrice = product.UnitPrice;

                    if (item.Quantity > product.UnitsInStock)
                        throw new Exception("На складе продукта не хватает");

                    productIdQuantity[product.ProductId] = item.Quantity;

                    repository.DecreaseUnitsInStock(product.ProductId, item.Quantity);

                    string request = $"INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity) VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity)";

                    SqlCommand command = new SqlCommand(request, connection);

                    SqlParameter OrderIdParam = new SqlParameter("@OrderId", order.OrderID);
                    command.Parameters.Add(OrderIdParam);

                    SqlParameter ProductIdParam = new SqlParameter("@ProductId", item.ProductId);
                    command.Parameters.Add(ProductIdParam);

                    SqlParameter UnitPriceParam = new SqlParameter("@UnitPrice", unitPrice);
                    command.Parameters.Add(UnitPriceParam);

                    SqlParameter QuantityParam = new SqlParameter("@Quantity", item.Quantity);
                    command.Parameters.Add(QuantityParam);

                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        /// <summary>
        /// Установка даты доставки заказа
        /// </summary>
        /// <param name="id">номер заказа</param>
        public int SetTheOrderDay(int id)
        {
            Order order = GetById(id);

            if (order.RequiredDate != null)
                return 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = $"UPDATE [Orders] SET RequiredDate = DATEADD(day, 5, GETDATE()) WHERE OrderID = {id}";

                SqlCommand command = new SqlCommand(request, connection);

                int p = command.ExecuteNonQuery();

                return p;
            }
        }

        /// <summary>
        /// Установка даты доставки заказа
        /// </summary>
        /// <param name="id">номер заказа</param>
        public int InstallOrderCompleted(int id)
        {
            Order order = GetById(id);

            if (order.ShippedDate != null)
                return 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string request = $"UPDATE [Orders] SET ShippedDate = GETDATE() WHERE OrderID = {id}";

                SqlCommand command = new SqlCommand(request, connection);

                int p = command.ExecuteNonQuery();

                return p;
            }
        }

        /// <summary>
        /// Вызов хранимой процедуры из базы данных
        /// </summary>
        /// <param name="customer">id покупателя</param>
        public Dictionary<string, int> CallingStoredProcedure(string customer)
        {
            Dictionary<string, int> keyValues = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"CustOrderHist";

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter CustomerIdParam = new SqlParameter("@CustomerID", customer);

                command.Parameters.Add(CustomerIdParam);

                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var t = (string)reader.GetValue(0);

                    var e = (int)reader.GetValue(1);

                    keyValues[t] = e;
                }
            }

            return keyValues;
        }
    }
}

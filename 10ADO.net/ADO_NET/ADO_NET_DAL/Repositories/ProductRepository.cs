using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_NET_DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    class ProductRepository : IProductRepository
    {
        private readonly string ConnectionString;
        public ProductRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Один продукт</returns>
        public Product GetById(int id)
        {
            Product product = new Product();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"SELECT ProductId, ProductName, QuantityPerUnit, UnitPrice, UnitsInStock  FROM Products WHERE ProductID = {id}";

                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == false)
                    throw new Exception("Продукта с таким идентификатором не существует");

                reader.Read();

                product.ProductId = (int)reader.GetValue(0);
                product.ProductName = DBNull.Value.Equals(reader.GetValue(1)) ? null : (string)reader.GetValue(1);
                product.QuantityPerUnit = DBNull.Value.Equals(reader.GetValue(2)) ? null : (string)reader.GetValue(2);
                product.UnitPrice = (decimal)reader.GetValue(3);
                product.UnitsInStock = (Int16)reader.GetValue(4);
            }
            return product;
        }

        /// <summary>
        /// Уменьшение количества продуктов на складе
        /// </summary>
        /// <param name="productid">идентификатор продукта</param>
        /// <param name="quantity">количество на которое надо умешить количество продуктов</param>
        public void DecreaseUnitsInStock(int productid, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"UPDATE Products SET UnitsInStock = UnitsInStock - {quantity} WHERE ProductID = {productid}";

                command.Connection = connection;

                int result = command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Увеличение количества продуктов на складе
        /// </summary>
        /// <param name="productid">идентификатор продукта</param>
        /// <param name="quantity">количество на которое надо увеличить количество продуктов</param>
        public void IncreaseUnitsInStock(int productid, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = $"UPDATE Products SET UnitsInStock = UnitsInStock + {quantity} WHERE ProductID = {productid}";

                command.Connection = connection;

                int result = command.ExecuteNonQuery();
            }
        }
    }
}

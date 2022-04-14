using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO_NET_DAL.Repositories
{
    class ProductRepository : IProductRepository
    {
        private readonly string ConnectionString;
        public ProductRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public void Create(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

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

        public void Update(Product t)
        {
            throw new NotImplementedException();
        }
    }
}

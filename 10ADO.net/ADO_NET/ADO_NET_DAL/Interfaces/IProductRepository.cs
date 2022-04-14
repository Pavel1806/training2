using ADO_NET_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Create(Product product);

        void Update(Product product);
    }
}

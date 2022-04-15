using ADO_NET_DAL.Model;
using ADO_NET_ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Create(ViewOrder order);

        void Update(ViewOrder order);

        int Delete(int id);
    }
}

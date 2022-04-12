using ADO_NET.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetOrder(int id);
        void CreateOrder(Order order);
    }
}

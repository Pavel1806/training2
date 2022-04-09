using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
    }
}

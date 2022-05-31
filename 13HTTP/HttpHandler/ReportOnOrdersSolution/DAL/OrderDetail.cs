using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrderDetail
    {
        public Order OrderID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Product ProductID { get; set; }

    }
}

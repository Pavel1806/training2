using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer CustomerID { get; set; }
    }
}

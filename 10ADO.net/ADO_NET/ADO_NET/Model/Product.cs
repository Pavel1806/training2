using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET.Model
{
    public class Product
    {
        public int ProductId{ get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }

    }
}

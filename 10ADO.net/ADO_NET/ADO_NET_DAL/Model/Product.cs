using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Model
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Model
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public OrderDetails(Product product)
        {
            Product = product;
        }

        public OrderDetails()
        {
        }
    }
}

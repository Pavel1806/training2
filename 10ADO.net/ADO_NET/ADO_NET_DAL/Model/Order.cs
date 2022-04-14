using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipCountry { get; set; }
        public Status OrderStatus { get; set; }
        public List<OrderDetails> orderDetails { get; set; } = new List<OrderDetails>();

        public enum Status
        {
            New,
            AtWork,
            Completed
        }
    }
}

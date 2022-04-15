using System;
using System.Collections.Generic;

namespace ADO_NET_ViewModel
{
    public class ViewOrder
    {
        public int OrderID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipCountry { get; set; }
        public List<ViewOrderDetails> orderDetails { get; set; } = new List<ViewOrderDetails>();
    }
}

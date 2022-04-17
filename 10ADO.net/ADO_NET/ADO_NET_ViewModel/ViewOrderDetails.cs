using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_ViewModel
{
    /// <summary>
    /// Продукт, входящий в ордер, приходящий с интерфейса
    /// </summary>
    public class ViewOrderDetails
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}

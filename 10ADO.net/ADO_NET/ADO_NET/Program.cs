using System;

namespace ADO_NET
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
            //orderRepository.GetAll();
            orderRepository.GetOrder(10785);
        }
    }
}

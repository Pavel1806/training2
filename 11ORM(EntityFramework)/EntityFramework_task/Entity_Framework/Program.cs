using System;

namespace Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository repository = new OrderRepository();

            repository.GetOrderByCategory();
        }
    }
}

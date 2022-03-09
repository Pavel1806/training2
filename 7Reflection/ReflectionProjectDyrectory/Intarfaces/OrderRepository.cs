using System;

namespace Intarfaces
{
    [OrderRepository(typeof(IOrderRepository))]
    public class OrderRepository : IOrderRepository
    {
        public void MessageOutput()
        {
            Console.WriteLine("Я репозиторий заказов - работаю с данными о заказах");
        }
    }
}

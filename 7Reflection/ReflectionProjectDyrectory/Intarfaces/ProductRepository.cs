using System;
using System.Collections.Generic;
using System.Text;

namespace Intarfaces
{
    
    class ProductRepository : IProductRepository
    {
        public void ConsoleWritline()
        {
            Console.WriteLine("Я репозиторий заказов - работаю с данными о заказах");
        }
    }
}

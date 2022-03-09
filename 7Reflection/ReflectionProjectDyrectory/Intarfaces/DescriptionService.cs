using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace Intarfaces
{
    
    
    public class DescriptionService
    {

        IOrderRepository orderRepository;

        IProductRepository productRepository;

        public DescriptionService()
        {
           
        }


        public void MethodIntarface()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());
            container.AddType(typeof(OrderRepository), typeof(IOrderRepository));


            orderRepository.MessageOutput();
            productRepository.ConsoleWritline();
        }
    }
}

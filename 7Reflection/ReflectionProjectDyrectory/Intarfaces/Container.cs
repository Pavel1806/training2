using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Intarfaces
{
    public class Container
    {
		public void AddAssembly(Assembly assembly)
		{


		}

		public void AddType(Type type)
		{ }

		public void AddType(Type type, Type baseType)
		{
			var t = (Type)Activator.CreateInstance(type);

			
			
		}

		public object CreateInstance(Type type)
		{
			return null;
		}

		public T CreateInstance<T>()
		{
			return default(T);
		}

		public void GetAttributes()
        {
			Assembly assembly = Assembly.GetExecutingAssembly();

			var t = assembly.GetTypes();

			var types = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == typeof(IOrderRepository))).ToList();

			foreach(var type in types)
            {
				var attributes = type.GetCustomAttributes();
				
				if(attributes != null)
                {
					IOrderRepository orderRepository = (IOrderRepository)Activator.CreateInstance(type);
				}
            }


		}


		public void Sample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var orderRepository = (OrderRepository)container.CreateInstance(typeof(OrderRepository));
			var productRepository = (ProductRepository)container.CreateInstance(typeof(ProductRepository));

			container.AddType(typeof(OrderRepository));
			container.AddType(typeof(OrderRepository), typeof(IOrderRepository));

			container.AddType(typeof(ProductRepository));
			container.AddType(typeof(ProductRepository), typeof(IProductRepository));
		}
	}
}

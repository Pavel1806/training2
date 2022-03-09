using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionIoc
{
    class Container
    {
		Assembly assembly;

		Type type;

		public void AddAssembly(Assembly assembl)
		{

			assembly = assembl;
		}

		public void AddType(Type type)
		{ }

		public void AddType(Type type, Type baseType)
		{
			
			var types = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == baseType)).ToList();

			foreach(var item in types)
            {
				if (item != type)
					throw new Exception($"{type} этот класс не реализует этот интерфейс {baseType}");

				var attributes = item.GetCustomAttributes();

				if (attributes.Count() == 0)
					throw new Exception($"{type} этот класс не содержит атрибутов");

				this.type = type;
				break;
			}
		}

		public object CreateInstance(Type type)
		{
			return null;
		}

		public T CreateInstance<T>()
		{

			T t = (T)Activator.CreateInstance(type);

			return t;
		}
	}
}

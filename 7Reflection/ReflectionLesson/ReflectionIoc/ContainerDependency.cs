using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionIoc
{
	/// <summary>
	/// Контейнер Зависимостей
	/// </summary>
	class ContainerDependency
	{
		Assembly assembly;

		Object type;

		/// <summary>
		/// Инициализация нового экземпляра класса Assembly
		/// </summary>
		/// <param name="assembl"></param>
		public void SetAssembly(Assembly assembl) 										 
		{
			assembly = assembl;
		}

		/// <summary>
		/// Создание экземпляра класса 
		/// </summary>
		/// <typeparam name="T">дочерний класс</typeparam>
		/// <typeparam name="V">реализуемый интерфейс</typeparam>
		public void AddType<T, V>()
		{
			var types = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == typeof(V))).ToList();

			if(types.Count() == 0)
				throw new Exception("");

				foreach (var item in types)
            {
				if (item != typeof(T))
					continue;

				var v = Activator.CreateInstance(item);

				this.type = v;
			}
		}

		/// <summary>
		/// Создание нового экземпляра класса типа T
		/// </summary>
		/// <typeparam name="T">Тип нового экземпляра</typeparam>
		/// <returns>Новый экземпляр типа T</returns>
		public T CreateInstance<T>()
		{
			var types = assembly.GetTypes().Where(x => x == typeof(T)).ToList();

			if (types.Count() == 0)
				throw new Exception("");

			T t = default(T);

			foreach (var item in types)
            {
				t = (T)Activator.CreateInstance(item, new object[] { type });
			}

			return t;
		}
	}
}

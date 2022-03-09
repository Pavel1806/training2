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

		Type type;

		/// <summary>
		/// Инициализация нового экземпляра класса Assembly
		/// </summary>
		/// <param name="assembl"></param>
		public void SetAssembly(Assembly assembl) 										 
		{
			assembly = assembl;
		}

		/// <summary>
		/// Проверка типов и атрибутов
		/// </summary>
		/// <param name="type"></param>
		/// <param name="baseType"></param>
		public void CheckType(Type type, Type baseType)
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

		/// <summary>
		/// Создание нового экземпляра класса типа T
		/// </summary>
		/// <typeparam name="T">Тип нового экземпляра</typeparam>
		/// <returns>Новый экземпляр типа T</returns>
		public T CreateInstance<T>()
		{ 
			T t = (T)Activator.CreateInstance(type);

			return t;
		}
	}
}

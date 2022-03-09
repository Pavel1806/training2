using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionIoc
{
	// TODO: Комментарии ко всем публичным методам и классам
	class Container // TODO: Некорректное название класса, под контейнером может подразумеваться очень много разных вещей
    {
		Assembly assembly;

		Type type;

		// TODO: Комментарии ко всем публичным методам и классам
		public void AddAssembly(Assembly assembl) // TODO: Некорректное название метода, метод ничего не добавляет, а устанавливает
												  // Соответственно должен называться SetAssembly, но и этот метод вроде как не нужен.
		{ // TODO: Строчкой ниже ненужный пробел

			assembly = assembl;
		}

		// TODO: Комментарии ко всем публичным методам и классам
		public void AddType(Type type) // TODO: Не используется
		{ }

		// TODO: Комментарии ко всем публичным методам и классам
		public void AddType(Type type, Type baseType)
		{ // TODO: Строчкой ниже ненужный пробел

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

		// TODO: Комментарии ко всем публичным методам и классам
		public object CreateInstance(Type type) // TODO: Нигде не используется
		{
			return null;
		}

		// TODO: Комментарии ко всем публичным методам и классам
		public T CreateInstance<T>()
		{ // TODO: Строчкой ниже ненужный пробел

			T t = (T)Activator.CreateInstance(type);

			return t;
		}
	}
}

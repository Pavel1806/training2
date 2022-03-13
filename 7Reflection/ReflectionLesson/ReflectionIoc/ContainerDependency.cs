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

		Dictionary<string, Object> combinedType;

		public ContainerDependency()
        {
			combinedType = new Dictionary<string, Object>();
        }

		/// <summary>
		/// Инициализация нового экземпляра класса Assembly
		/// </summary>
		/// <param name="assembl"></param>
		public void SetAssembly(Assembly assembl) 										 
		{
			assembly = assembl;
		}

		/// <summary> // TODO: Некорректный комментарий
		/// Создание экземпляра класса 
		/// </summary>
		/// <typeparam name="T">дочерний класс</typeparam>
		/// <typeparam name="V">реализуемый интерфейс</typeparam>
		public void AddType<T, V>()
		{
			var type = typeof(T);

			var attributes = type.CustomAttributes;

			if (attributes.Count() == 0)
				throw new Exception($"У {typeof(T)} нет никаких атрибутов");

			foreach (var attribute in attributes) // TODO: Можно заменить на выражение Linq
            {

				if (attribute.AttributeType.Name.IndexOf("Export") == -1) //TODO: В этом случае лучше использовать Contains 
					throw new Exception($"У {typeof(T)} нет нужного атрибута");
			}

            var types = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == typeof(V))).ToList();

            if (types.Count() == 0)
                throw new Exception($"{typeof(T)} не реализует {typeof(V)}");

            foreach (var item in types)
            {
                if (item != typeof(T))
                    throw new Exception($"{typeof(T)} не реализует {typeof(V)}");

                var instance = (T)Activator.CreateInstance(item);

                var name = item.Name;

                this.combinedType.Add(name, instance); // TODO: Ошибка, инстанс обьекта должен создаваться в момент, когда у контейнера запрашивают его
                                                       // Например, бывают контейнеры, в ASP .Net Core с этим встретишься, когда есть разные способы внедрения
                                                       // 1. Singletone - паттерн, когда обьект класса может быть только в единичном экземпляре на всю программу
                                                       // 2. Для каждого контроллера создаётся свой уникальный обьект
                                                       // 3. Для каждого запроса создаётся свой уникальный обьект
                                                       // Описанные выше способы внедрения зависимостей предлагаются ASP .Net Core, но
                                                       // В целом, суть всегда одна, есть метод маппинга, а есть метод создания инстанса класса
                                                       // где также создаются обьекты-зависимости.
            }
        }

		/// <summary>
		/// Создание нового экземпляра класса типа T
		/// </summary>
		/// <typeparam name="T">Тип нового экземпляра</typeparam>
		/// <returns>Новый экземпляр типа T</returns>
		public T CreateInstance<T>() 
		{
			var type = typeof(T);

            if (type == null)
                throw new Exception($"Такого типа класса не существует");

            var attributes = type.CustomAttributes;

            foreach(var attribute in attributes) // Можно заменить на выражение Linq
            {
                if (attribute.AttributeType.Name.IndexOf("ImportConstructor") == -1) //TODO: В этом случае лучше использовать Contains 
                    throw new Exception($"У {typeof(T)} нет нужного атрибута");
            }

            T t = default(T);

		    var ctors = type.GetConstructors();

            foreach (var ctor in ctors)
            {
                var paramsCtor = ctor.GetParameters();

                List<object> listParam = new List<object>();

                foreach (var param in paramsCtor)
                {
                    var typesParam = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == param.ParameterType)).ToList();

                    foreach (var typeParam in typesParam)
                    {
                        foreach (var k in combinedType) // TODO: Описал проблему и почему такой подход неверен в методе выше.
                        {
                            if (typeParam.Name == k.Key)
                            {
                                listParam.Add(k.Value);
                            }
                        }
                    }
                }

                Object[] arrayParam = new object[listParam.Count];

                for (int i = 0; i < arrayParam.Count(); i++)
                {
                    arrayParam[i] = listParam[i];
                }

                t = (T)Activator.CreateInstance(type, arrayParam);
            }
            return t;
		}
	}
}

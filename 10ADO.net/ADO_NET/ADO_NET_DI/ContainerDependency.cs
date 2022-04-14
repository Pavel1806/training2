using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ADO_NET_DI
{
    class ContainerDependency
    {
        Assembly assembly;

        List<string> listClass;

        Dictionary<Type, Type> dictionaryClass;

        public ContainerDependency()
        {
            listClass = new List<string>();
            dictionaryClass = new Dictionary<Type, Type>();
        }


        /// <summary>
        /// Инициализация нового экземпляра класса Assembly
        /// </summary>
        /// <param name="assembl"></param>
        public void SetAssembly(Assembly assembl)
        {
            assembly = assembl;
        }
        public void AddType<T, V>()
        {
            var type = typeof(T);

            var attributes = type.CustomAttributes;

            if (!attributes.Any())
                throw new Exception($"У {typeof(T)} нет никаких атрибутов");

            //var attribute = attributes.Where(x => x.AttributeType.Equals(typeof(ExportAttribute)));

            //if (!attribute.Any())
            //    throw new Exception($"У {typeof(T)} нет нужного атрибута");

            var types = assembly.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Any(t => t == typeof(V)));

            if (!types.Any())
                throw new Exception($"{typeof(T)} не реализует {typeof(V)}");

            var type1 = types.Where(x => x.Equals(typeof(T)));

            if (!type1.Any())
                throw new Exception($"{typeof(T)} не реализует {typeof(V)}");

            dictionaryClass.Add(typeof(T), typeof(V));
        }
    }
}

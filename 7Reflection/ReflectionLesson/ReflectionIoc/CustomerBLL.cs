using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Класс слоя бизнес логики
    /// </summary>
    [Export(typeof(ICustomerBLL))]
    class CustomerBLL : ICustomerBLL
    {
        /// <summary>
        /// Метод выводит название класса
        /// </summary>
        public void ConsoleWrite()
        {
            Console.Write("CustomerBLL");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Класс слоя данных
    /// </summary>
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
        /// <summary>
        /// Метод который выводит на консоль сообщение
        /// </summary>
        public void ConsoleWritline()
        {
            Console.WriteLine("ПРивет");
        }
    }
}

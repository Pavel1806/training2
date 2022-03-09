using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Интерфейс слоя данных
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Метод который выводит на консоль сообщение
        /// </summary>
        public void ConsoleWritline();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Класс слоя данных
    /// </summary>
    public class CustomerDAL
    {
        private ICustomerBLL customerBLL;
        public CustomerDAL (ICustomerBLL customerBLL)
        {
            this.customerBLL = customerBLL;
        }
        /// <summary>
        /// Метод выводит название класса СustomerBLL
        /// </summary>
        public void ConsoleWrite()
        {
            customerBLL.ConsoleWrite();
        }
        /// <summary>
        /// Метод выводит название данного класса
        /// </summary>
        public void ConsoleWriteline()
        {
            Console.WriteLine("CustomerDAL");
        }
    }
}

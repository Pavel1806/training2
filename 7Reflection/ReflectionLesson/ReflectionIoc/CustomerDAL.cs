using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Класс слоя данных
    /// </summary>
    [ImportConstructor]
    public class CustomerDAL
    {
        private ICustomerBLL customerBLL;

        private ICustomerBLL2 customerBLL2;

        private ICustomerBLL3 customerBLL3;
        public CustomerDAL (ICustomerBLL customerBLL, ICustomerBLL2 customerBLL2, ICustomerBLL3 customerBLL3)
        {
            this.customerBLL = customerBLL;
            this.customerBLL2 = customerBLL2;
            this.customerBLL3 = customerBLL3;
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

        public void CWrite()
        {
            customerBLL2.CWrite();
        }

        public void ConWrite()
        {
            customerBLL3.ConWrite();
        }
    }
}

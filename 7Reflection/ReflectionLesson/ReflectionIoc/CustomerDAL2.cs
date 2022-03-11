using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    class CustomerDAL2
    {
        private ICustomerBLL customerBLL;

        private ICustomerBLL3 customerBLL3;
        public CustomerDAL2(ICustomerBLL customerBLL, ICustomerBLL3 customerBLL3)
        {
            this.customerBLL = customerBLL;
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

        public void ConWrite()
        {
            customerBLL3.ConWrite();
        }
    }
}

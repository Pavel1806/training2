using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    /// <summary>
    /// Класс слоя бизнес логики
    /// </summary>
    class CustomerBLL
    {
        public ICustomerDAL customerDAL;

        /// <summary>
        /// Конструктор внедрения зависимостей
        /// </summary>
        /// <param name="customerDAL"> переменная класса слоя данных</param>
        public CustomerBLL(ICustomerDAL customerDAL) 
        {
            this.customerDAL = customerDAL;
        }

    }
}

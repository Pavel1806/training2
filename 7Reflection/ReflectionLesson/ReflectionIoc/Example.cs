using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    class Example
    {
        Processing Processing = new Processing();
        ICustomerDAL customerDAL;


        public void Metod()
        {
          customerDAL = Processing.Customer;

            customerDAL.ConsoleWritline();
        }

        

    }
}

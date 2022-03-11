using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    class CustomerBLL2 : ICustomerBLL2
    {
        public void CWrite()
        {
            Console.Write("CustomerBLL2");
        }
    }
}

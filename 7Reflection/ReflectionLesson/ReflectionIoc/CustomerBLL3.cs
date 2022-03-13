using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    [Export(typeof(ICustomerBLL3))]
    class CustomerBLL3 : ICustomerBLL3
    {
        public void ConWrite()
        {
            Console.Write("CustomerBLL3");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
        public void ConsoleWritline()
        {
            Console.WriteLine("Привет");
        }
    }
}

using System;
using System.Reflection;

namespace ReflectionIoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerDependency();

            container.SetAssembly(Assembly.GetExecutingAssembly());

            container.CheckType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var castDAL = container.CreateInstance<ICustomerDAL>();

            CustomerBLL customerBLL = new CustomerBLL(castDAL);

            customerBLL.customerDAL.ConsoleWritline();
        }
    }
}

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

            container.AddType<CustomerBLL, ICustomerBLL>();

            var castDAL = container.CreateInstance<CustomerDAL>();

            castDAL.ConsoleWrite();

            castDAL.ConsoleWriteline();
        }
    }
}

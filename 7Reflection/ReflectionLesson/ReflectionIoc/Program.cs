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

            //container.AddType<CustomerBLL, ICustomerBLL2>();   //для проверки ошибок

            container.AddType<CustomerBLL, ICustomerBLL>();

            container.AddType<CustomerBLL2, ICustomerBLL2>();

            container.AddType<CustomerBLL3, ICustomerBLL3>();

            var castDAL = container.CreateInstance<CustomerDAL>();

            var castDAL2 = container.CreateInstance<CustomerDAL2>();

            castDAL.ConsoleWrite();

            castDAL.ConsoleWriteline();

            castDAL.CWrite();

            castDAL.ConWrite();

            castDAL2.ConsoleWrite();

            castDAL2.ConsoleWriteline();

            castDAL2.ConWrite();
        }
    }
}

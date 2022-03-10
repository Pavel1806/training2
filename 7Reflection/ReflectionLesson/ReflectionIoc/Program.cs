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

            container.CheckType(typeof(CustomerDAL), typeof(ICustomerDAL)); // TODO: А зачем проверять тип? Чаще делается определение зависимостей на ранних этапах
                                                                            // Например: "Такой-то интерфейс реализуется таким-то классом, а другой вот таким..."

            var castDAL = container.CreateInstance<ICustomerDAL>(); // TODO: У класса реализующего ICustomerDAL нет никаких зависимостей которые
                                                                    // были бы внедрены через свойства или конструктор (чаще внедряют через конструктор).
                                                                    // Необходимо исправить это.

            CustomerBLL customerBLL = new CustomerBLL(castDAL); // TODO: Подход с IoC контейнером зачастую выглядит так, что ты запрашивает обьект
                                                                // И он возвращается со всеми необходимыми зависимостями

            customerBLL.customerDAL.ConsoleWritline();
        }
    }
}

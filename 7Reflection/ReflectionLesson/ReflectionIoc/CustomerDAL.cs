using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    // TODO: Комментарии ко всем публичным методам и классам
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL // TODO: В классе нет внедрения зависимостей через конструктор или свойства.
    {
        // TODO: Комментарии ко всем публичным методам и классам
        public void ConsoleWritline() // TODO: Непонятное название
        {
            Console.WriteLine("Привет");
        }
    }
}

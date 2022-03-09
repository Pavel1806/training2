using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ReflectionIoc
{
    // TODO: Комментарии ко всем публичным методам и классам
    class Processing // TODO: Ни о чём не говорящее название для класса + можно обойтись без него, концепцию описал в Program.cs
    {
        ICustomerDAL customer; // TODO: Некорректное название для поля, это ведь customerDAL, а не customer.

        // TODO: Комментарии ко всем публичным методам и классам
        public ICustomerDAL Customer // TODO: Некорректное название для свойства, это ведь CustomerDAL, а не Customer.
        {
            get
            {
                if (customer == null)
                    customer = InstanceClass();
                return customer;
            }
        }

        // TODO: Комментарии ко всем публичным методам и классам
        public ICustomerDAL InstanceClass() // TODO: Методы не должны называться существительными! Это действия - глаголы :)
                                            // Более того, название ни о чём не говорит.
        {
            var container = new Container();

            container.AddAssembly(Assembly.GetExecutingAssembly());

            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var castDAL = container.CreateInstance<ICustomerDAL>();

            return castDAL;
        }
        
    }
}

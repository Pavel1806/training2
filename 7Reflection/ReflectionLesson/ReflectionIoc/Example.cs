using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    // TODO: Комментарии ко всем публичным методам и классам
    class Example // TODO: Ни о чём не говорящее название для класса, необходимо исправить
    {
        Processing Processing = new Processing();
        ICustomerDAL customerDAL;

        // TODO: Комментарии ко всем публичным методам и классам
        public void Metod() // TODO: Ошибка в названии Method + ни о чём не говорящее название. Методы не должны называться существительным
                            // Это действия, которые в большинстве случаев должны называться глаголами
        {
          customerDAL = Processing.Customer;

            customerDAL.ConsoleWritline();
        }

        

    }
}

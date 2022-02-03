using ConvertStringToNumber;
using System;

namespace ErrorHandlingModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово");
            var wordConsole = Console.ReadLine();

            try
            {
                Console.WriteLine(wordConsole[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine("Вы ничего не ввели!"); // TODO: Не должно быть логики на исключениях. Исключения для другого.
            }

            int number = 0; // TODO: Стоит перенести ближе к использованию.
            try
            {
                number = ConvertString.ToInt("-658678"); // TODO: Почему бы не запросить у пользователя число?
                Console.WriteLine(number);
            }
            catch(Exception e) // TODO: Как насчёт того чтобы проинформировать пользователя в о том, что конкретно пошло не так?
            {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}

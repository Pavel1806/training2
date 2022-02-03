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
                Console.WriteLine("Вы ничего не ввели!"); 
            }

            int number = 0;
            try
            {
                number = ConvertString.ToInt("-658678");
                Console.WriteLine(number);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}

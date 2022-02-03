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
                Console.WriteLine(e.Message); 
            }

            int t =  ConvertString.ToInt("eruut");

            Console.WriteLine(t);
        }
    }
}

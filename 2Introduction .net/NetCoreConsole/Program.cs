using System;

namespace NetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ваше имя");

            string name = Console.ReadLine();

            Console.WriteLine($"Hello {name}!");
        }
    }
}

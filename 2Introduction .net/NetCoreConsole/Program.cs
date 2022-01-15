using System;
using Multitargeting2;

namespace NetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ваше имя");

            string name = Console.ReadLine();

            //Console.WriteLine($"Hello {name}!");

            StringWithTime stringWithTime = new StringWithTime();
            string str = stringWithTime.OutputNameDatetime(name);

            Console.WriteLine(str);
        }
    }
}

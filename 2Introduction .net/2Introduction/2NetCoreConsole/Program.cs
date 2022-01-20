using System;
using _2MultitargetingStandart;

namespace _2NetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ваше имя");

            string name = Console.ReadLine();

            StringWithTime stringWithTime = new StringWithTime();
            string str = stringWithTime.OutputNameDatetime(name);

            Console.WriteLine(str);
        }
    }
}

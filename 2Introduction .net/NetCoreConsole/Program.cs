using System;
using MultitargetingStandart2._0;

namespace NetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ваше имя");

            string name = Console.ReadLine();

            //Console.WriteLine($"Hello {name}!"); // TODO: Мертвого кода в репозитории быть не должно.

            StringWithTime stringWithTime = new StringWithTime();
            string str = stringWithTime.OutputNameDatetime(name);

            Console.WriteLine(str);
        }
    }
}

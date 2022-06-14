using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FibonacciSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagerCashe casheRedis = new ManagerCashe(new NumberRedisCashe("localhost"));

            ManagerCashe casheMemory = new ManagerCashe(new NumberMemoryCashe());

            Console.WriteLine("Кеш редис");
            
            casheRedis.Cashe();

            Console.WriteLine("Кеш memory");

            casheMemory.Cashe();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FibonacciSeries
{
    class ManagerCashe
    {
        INumberCashe cashe;
        public ManagerCashe(INumberCashe cashe)
        {
            this.cashe = cashe;
        }

        public void Cashe()
        {
            int i = 0;
            int j = 1;
            int x = 0;
            string fib = "fibonacci";

            Console.WriteLine(i);

            Console.WriteLine(j);

            List<int> numbers = new List<int>();

            while (x < 1000)
            {

                numbers.Add(x);

                cashe.Set(numbers, fib, DateTimeOffset.Now.AddSeconds(15));

                Thread.Sleep(3000);

                IEnumerable<int> numberCashe = cashe.Get(fib);

                if (numberCashe == null)
                    throw new Exception("Кеш пустой");

                if (numberCashe.LastOrDefault() != x)
                    throw new Exception("Кеш неверный");

                x = i + j;

                Console.WriteLine(x);

                i = j;
                j = x;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FibonacciSeries
{
    public interface INumberCashe
    {
        IEnumerable<int> Get(string key);

        void Set(IEnumerable<int> numbers, string key, TimeSpan timespan);

    }
}

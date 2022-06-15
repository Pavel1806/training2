using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace FibonacciSeries
{
    class NumberMemoryCashe : INumberCashe
    {
        ObjectCache cashe = MemoryCache.Default;

        public IEnumerable<int> Get(string key)
        {
            return (IEnumerable<int>)cashe.Get(key);
        }

        public void Set(IEnumerable<int> numbers, string key, TimeSpan timespan)
        {
            cashe.Set(key, numbers, DateTimeOffset.Now.Add(timespan));
        }
    }
}

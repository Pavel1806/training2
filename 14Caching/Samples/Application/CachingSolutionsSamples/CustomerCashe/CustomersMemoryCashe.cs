using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples.CustomerCashe
{
    class CustomersMemoryCashe : ICache<Customer>
    {
		ObjectCache cache = MemoryCache.Default;
		string prefix = "Cache_Castomers";

		public IEnumerable<Customer> Get(string forUser)
		{
			return (IEnumerable<Customer>)cache.Get(prefix + forUser);
		}

		public void Set(string forUser, IEnumerable<Customer> customers)
		{
			cache.Set(prefix + forUser, customers, ObjectCache.InfiniteAbsoluteExpiration);
		}
	}
}

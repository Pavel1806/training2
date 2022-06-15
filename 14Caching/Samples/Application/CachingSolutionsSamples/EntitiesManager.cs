using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public class EntitiesManager<T> where T : class
	{
		private readonly ICache<T> cache;

		public EntitiesManager(ICache<T> cache)
		{
			this.cache = cache;
		}

		public IEnumerable<T> GetEntities()
		{
			Console.WriteLine($"Get {typeof(T).Name}");

			var user = Thread.CurrentPrincipal.Identity.Name;
			IEnumerable<T> entities = cache.Get(user);

			if (entities == null)
			{
				Console.WriteLine("From DB");

				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;

					if (typeof(T) == typeof(Category))
                    {
						entities = (IEnumerable<T>)dbContext.Categories.ToList();
					}
                    else if(typeof(T) == typeof(Customer))
					{
						entities = (IEnumerable<T>)dbContext.Customers.ToList();
					}
					else if (typeof(T) == typeof(Product))
					{
						entities = (IEnumerable<T>)dbContext.Products.ToList();
					}

					cache.Set(user, entities);
				}
			}

			return entities;
		}
	}
}

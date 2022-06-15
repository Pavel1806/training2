using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;
using CachingSolutionsSamples.CategoriesCashe;
using CachingSolutionsSamples.CustomerCashe;
using CachingSolutionsSamples.EmployeesCashe;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCacheCategory()
		{
			var categoryManager = new EntitiesManager<Category>(new CategoriesMemoryCache());  

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCacheCategory()
		{
			var categoryManager = new EntitiesManager<Category>(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void MemoryCacheCustomer()
		{
			var customerManager = new EntitiesManager<Customer>(new CustomersMemoryCashe());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(customerManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCacheCustomer()
		{
			var customerManager = new EntitiesManager<Customer>(new CustomersRedisCashe("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(customerManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void MemoryCacheEmployee()
		{
			var customerManager = new EntitiesManager<Product>(new ProductsMemoryCashe());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(customerManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCacheEmployee()
		{
			var customerManager = new EntitiesManager<Product>(new ProductsRedisCashe("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(customerManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}
	}
}

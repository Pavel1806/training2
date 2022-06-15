using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public interface ICache<T> where T : class
	{
		IEnumerable<T> Get(string forUser);
		void Set(string forUser, IEnumerable<T> t);
	}
}

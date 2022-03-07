using MyIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCSample
{
    [ImportConstructor]
	public class CustomerBLL
	{
       public ICustomerDAL CustomerDAL { get; set; }
       public Logger logger { get; set; }
	}
}

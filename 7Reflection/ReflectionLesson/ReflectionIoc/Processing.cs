using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ReflectionIoc
{
    class Processing
    {
        ICustomerDAL customer;

        public ICustomerDAL Customer
        {
            get
            {
                if (customer == null)
                    customer = InstanceClass();
                return customer;
            }
        }

        public ICustomerDAL InstanceClass()
        {
            var container = new Container();

            container.AddAssembly(Assembly.GetExecutingAssembly());

            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var castDAL = container.CreateInstance<ICustomerDAL>();

            return castDAL;
        }
        
    }
}

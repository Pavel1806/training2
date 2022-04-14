using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ADO_NET_DI
{
    public class ContainerDI
    {
        void Start()
        {
            var container = new ContainerDependency();

            container.SetAssembly(Assembly.GetExecutingAssembly());

            //container.AddType<OrderRepository, IOrderRepository>();
        }
       
    }
}

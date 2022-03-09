using System;
using System.Collections.Generic;
using System.Text;

namespace Intarfaces
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ProductRepositoryAttribute : Attribute
    {
        ProductRepository productRepository = new ProductRepository();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OrderRepositoryAttribute : Attribute
    {
        public OrderRepositoryAttribute()
        { }

        public OrderRepositoryAttribute(Type contract)
        {
            Contract = contract;
        }

        public Type Contract { get; private set; }

        //void orderAttr()
        //{
        //    Container container = new Container();
        //    container.GetAttributes();
        //}
    }
}

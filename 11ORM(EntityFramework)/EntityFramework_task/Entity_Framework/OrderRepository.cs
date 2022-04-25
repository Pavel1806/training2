using Entity_Framework.Context;
using Entity_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity_Framework
{
    class OrderRepository : IOrderRepository
    {
        public void GetOrderByCategory()
        {
            NorthwindContext context = new NorthwindContext();

            var r = context.OrderDetails.AsEnumerable().GroupBy(x=>x.OrderId).ToList();

            var p = context.Orders.Join(context.OrderDetails, p => p.OrderId, c => c.OrderId,
                (p, c) => new
                {
                    Category = c.Product.Category.CategoryId,
                    Product = c.Product.ProductName,
                    OrderId = p.OrderId,
                    OrderDetail = p.OrderDetails
                }).Where(c => c.Category == 1).AsEnumerable().GroupBy(x => x.OrderId).Select(c=> new { 
                           OrderId = c.Key,
                           OrderDetails = c.Select(p=>p)
                }).ToList();

            var t = context.Orders.Join(context.OrderDetails, p => p.OrderId, c=>c.OrderId, 
                (p, c) => new { 
                    Category = c.Product.Category.CategoryId,
                    Product = c.Product.ProductName,
                    OrderId= p.OrderId
                }).ToList();

            foreach(var orderId in p)
            {
                Console.WriteLine($"{orderId.OrderId}");

                foreach (var item in orderId.OrderDetails)
                {
                    Console.WriteLine($"{item.Product}--{item.Category}");
                }
            }
        }
    }
}

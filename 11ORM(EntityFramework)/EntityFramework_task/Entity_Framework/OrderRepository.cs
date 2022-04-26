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

            var order = context.OrderDetails.Where(c => c.Product.CategoryId == 1).AsEnumerable().GroupBy(x => x.OrderId).Select(c => new {
                    OrderId = c.Key,
                    OrderDetails = context.Orders.Join(context.OrderDetails, p => p.OrderId, c => c.OrderId,
                        (p, c) => new
                        {
                            Category = c.Product.Category.CategoryId,
                            Product = c.Product.ProductName,
                            UnitPrice = c.UnitPrice,
                            Quantity = c.Quantity,
                            OrderId = p.OrderId,
                            Customer = p.Customer.CompanyName
                        }).Where(y => y.OrderId == c.Key)
            }).ToList();

            foreach(var orderId in order) 
            {
                Console.WriteLine($"{orderId.OrderId}");

                foreach (var item in orderId.OrderDetails)
                {
                    Console.WriteLine($"{item.Customer}--{item.Product}--{item.UnitPrice}--{item.Quantity}");
                }
            }
        }
    }
}

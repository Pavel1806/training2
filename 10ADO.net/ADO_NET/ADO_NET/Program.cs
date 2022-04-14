using System;
using System.Collections.Generic;
using ADO_NET.Interfaces;
using ADO_NET.Model;
using ADO_NET.Repositories;

namespace ADO_NET
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<Order> orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
            IRepository<Product> productRepository = new ProductRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
            //orderRepository.GetAll();
            orderRepository.GetById(10785);
            //orderRepository.Create(new Order
            //{
            //    orderDetails = new List<OrderDetails>() {
            //        new OrderDetails(){ProductId = 2, Quantity = 1},
            //        new OrderDetails(){ProductId = 75, Quantity = 1},
            //        new OrderDetails(){ProductId = 45, Quantity = 1},
            //        new OrderDetails(){ProductId = 66, Quantity = 1},
            //        new OrderDetails(){ProductId = 34, Quantity = 1}
            //      },
            //});

            //productRepository.GetById(11);


        }
    }
}

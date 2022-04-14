using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using ADO_NET_DAL.Repositories;
using ADO_NET_DI;
using ADO_NET_ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ADO_NET_TESTs
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void GetAll_Not_Null()
        {

            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            //orderRepository.Create(new ViewOrder() 
            //{ 
            //   ShipAddress="",
            //    ShipCity="",
            //     ShipCountry="",
            //      ShipName="",
            //       ShipRegion="",
            //        orderDetails = new List<ViewOrderDetails>()
            //        {
            //            new ViewOrderDetails(){ ProductId=23, Quantity=1},
            //            new ViewOrderDetails(){ ProductId=45, Quantity=1},
            //            new ViewOrderDetails(){ ProductId=56, Quantity=1}
            //        }
            //});
            //orderRepository.GetById(23434);
            var t = orderRepository.GetAll();

            List<Order> orders = (List<Order>)t;

            CollectionAssert.AllItemsAreNotNull(orders);
        }

        [TestMethod]
        public void GetById_Not_Null()
        {

            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            Order order = orderRepository.GetById(23434);

            Order expected = null;

            Assert.AreEqual(expected, order);
        }


        [TestMethod]
        public void GetAll()
        {
            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            orderRepository.Create(new ViewOrder()
            {
                ShipAddress = "",
                ShipCity = "",
                ShipCountry = "",
                ShipName = "",
                ShipRegion = "",
                orderDetails = new List<ViewOrderDetails>()
                    {
                        new ViewOrderDetails(){ ProductId=23, Quantity=1},
                        new ViewOrderDetails(){ ProductId=45, Quantity=1},
                        new ViewOrderDetails(){ ProductId=56, Quantity=1}
                    }
            });
        }
    }
}

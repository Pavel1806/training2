using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using ADO_NET_DAL.Repositories;
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
        public void Create_1()
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
                        new ViewOrderDetails(){ ProductId=1, Quantity=1},
                        new ViewOrderDetails(){ ProductId=2, Quantity=1},
                        new ViewOrderDetails(){ ProductId=3, Quantity=14}
                    }
            });

        }

        [TestMethod]
        public void Delete_Not_Null()
        {

            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            int actual = orderRepository.Delete(11092);

            int expected = 0;

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Update()
        {

            IOrderRepository orderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");

            orderRepository.Update(new ViewOrder()
            {
                OrderID = 10250,
                ShipAddress = "",
                ShipCity = "",
                ShipCountry = "",
                ShipName = "",
                ShipRegion = "",
                orderDetails = new List<ViewOrderDetails>()
                    {
                        new ViewOrderDetails(){ ProductId=41, Quantity=10},
                        new ViewOrderDetails(){ ProductId=65, Quantity=10},
                        new ViewOrderDetails(){ ProductId=51, Quantity=10},
                        new ViewOrderDetails(){ ProductId=3, Quantity=10},
                        new ViewOrderDetails(){ ProductId=4, Quantity=10}
                    }
            });

            //Assert.AreNotEqual(expected, actual);
        }


    }
}

using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Model;
using ADO_NET_DAL.Repositories;
using ADO_NET_ViewModel;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace ADO_NET_TESTs
{
    [TestClass]
    public class UnitTest1
    {
        string ConnectionString { get; set; }

        IOrderRepository OrderRepository { get; set; }

        [TestInitialize]
        public void Testinitialize()
        {
            //Ñonnectionstring ñonnectionstring = new Ñonnectionstring();

            //var ConnectionString = ñonnectionstring.Get();

            OrderRepository = new OrderRepository(@"Data Source=DESKTOP-5V2J771\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
        }

        [TestMethod]
        public void GetAll_Not_Null()
        {
            var t = OrderRepository.GetAll();

            List<Order> orders = (List<Order>)t;

            CollectionAssert.AllItemsAreNotNull(orders);
        }

        [TestMethod]
        public void GetById_Null()
        {
            Order order = OrderRepository.GetById(23434);

            Order expected = null;

            Assert.AreEqual(expected, order);
        }


        [TestMethod]
        public void Create_Not_Null()
        {
            int orderId = OrderRepository.Create(new ViewOrder()
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
                        new ViewOrderDetails(){ ProductId=3, Quantity=1}
                    }
            });

            int expected = 0;

            Assert.AreNotEqual(expected, orderId);
        }

        [TestMethod]
        public void Delete_Not_Null()
        { 
            int actual = OrderRepository.Delete(11082);

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

        [TestMethod]
        public void SetTheOrderDay()
        {
            int actual = OrderRepository.SetTheOrderDay(11081);

            int expected = 1;

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void InstallOrderCompleted()
        {
            int actual = OrderRepository.InstallOrderCompleted(11081);

            int expected = 1;

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void CallingStoredProcedure_Not_Null()
        {
            var dict = OrderRepository.CallingStoredProcedure("CHOPS");

            CollectionAssert.AllItemsAreNotNull(dict);         
        }
    }
}

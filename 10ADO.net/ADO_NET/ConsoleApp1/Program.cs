using ADO_NET_DAL.Interfaces;
using ADO_NET_DAL.Repositories;
using System;
using System.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Сonnectionstring сonnectionstring = new Сonnectionstring();

            var connectionString = сonnectionstring.Get();

            IOrderRepository orderRepository = new OrderRepository(connectionString);

            var orders = orderRepository.GetAll();

            var order = orderRepository.GetById(23434);
        }
    }

    public class Сonnectionstring
    {
        public string Get()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;

            return connectionString;
        }
        
    }
}

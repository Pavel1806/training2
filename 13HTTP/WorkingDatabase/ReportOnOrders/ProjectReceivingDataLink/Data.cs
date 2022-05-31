using DAL.Context;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectReceivingDataLink
{
    public class Data
    {
        
        public Data()
        {
            
        }

        public DbContextOptions<NorthwindContext> AppContext()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            //получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            return options;
        }

        public IEnumerable<Orders> GetData(int countSkip, int countTake, string id, DateTime dateFrom)
        {
            NorthwindContext context = new NorthwindContext(AppContext());

            IEnumerable<Orders> orders = context.Orders;

            if (!string.IsNullOrEmpty(id))
                orders = orders.Where(x => x.CustomerId == id);

            if (dateFrom != DateTime.MinValue)
                orders = orders.Where(x => x.OrderDate >= dateFrom);

            if (countSkip != 0)
                orders = orders.Skip(countSkip);

            if(countTake != 0)
                orders = orders.Take(countTake);

           
            foreach (var item in orders)
            {
                yield return item;
            }
            
        }

    }

       
}


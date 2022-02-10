// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Task 1")]
		[Title("Where - Task 1")]
		[Description("Список клиентов у которых сумма всех заказов больше orderAmmount")]
		public void Linq1()
        {
            int orderAmmount = 35400;
            
            var listOfSortedClients = dataSource.Customers.Where(x => x.Orders.Sum(y => y.Total) > orderAmmount);

            foreach (var item in listOfSortedClients)
            {
                Console.WriteLine(item.CompanyName);
            }
        }

		[Category("Task 2")]
		[Title("Where - Task 2.1")]
		[Description("Список поставщиков, находящихся в той же стране и том же городе")]

		public void Linq2_1()
        {
            var listCustomer = dataSource.Customers;

            foreach (var item in listCustomer)
            {
                var listOfSortedSuppliers = dataSource.Suppliers.Where(x => x.Country == item.Country).Where(y => y.City == item.City);
                
                Console.WriteLine();
                Console.WriteLine($"Имя клиента {item.CompanyName}");
                Console.WriteLine();

                if (listOfSortedSuppliers.Count() != 0)
                {
                    Console.WriteLine("Список поставщиков");
                    foreach (var x in listOfSortedSuppliers)
                    {
                        Console.WriteLine(x.SupplierName);
                    }
                }
                else
                {
                    Console.WriteLine("Поставщиков в той же стране и в том же городе, что и клиент, не найдено");
                }
            }
        }

        [Category("Task 2")]
        [Title("Where - Task 2.2")]
        [Description("Список поставщиков, находящихся в той же стране и том же городе")]

        public void Linq2_2()
        {
          
            var listOfSortedCustomers = 
                dataSource.Customers.GroupJoin(
                dataSource.Suppliers, 
                t => new {t.Country, t.City}, 
                p => new { p.Country, p.City },
                (custom, spls) => new 
                {
                   Name = custom.CompanyName,
                   Suppliers = spls.Select(p=>p.SupplierName)
                });
            
            foreach (var item in listOfSortedCustomers)
            {

                Console.WriteLine();
                Console.WriteLine($"Имя клиента {item.Name}");
                Console.WriteLine();
                
                if (item.Suppliers.Count() != 0)
                {
                    Console.WriteLine("Список поставщиков находящихся в том же городе");
                    foreach (var x in item.Suppliers)
                    {
                        Console.WriteLine(x);
                    }
                }
                else
                {
                    Console.WriteLine("Поставщиков в том же городе, что и клиент, не найдено");
                }

            }


        }
        [Category("Task 3")]
        [Title("Where - Task 3")]
        [Description("Все клиенты, у которых были заказы, превосходящие по сумме величину X")]

        public void Linq3()
        {
            var listOfSortedCustomers = dataSource.Customers.Where(x => x.Orders.Any(y=>y.Total > 5544));

            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine(item.CompanyName);
            }
        }

        [Category("Task 4")]
        [Title("Where - Task 4.1")]
        [Description("Список клиентов с указанием, начиная с какого месяца какого года они стали клиентами")]
        public void Linq4_1()
        {
            var listOfSortedCustomers = dataSource.Customers.Select(x => new
            {
                Customer = x.CompanyName,
                Date = x.Orders.Length != 0 ? x.Orders.Min(s => s.OrderDate) : new DateTime()
            });

            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine($"{item.Customer}--{item.Date}");
            }
        }

        [Category("Task 4")]
        [Title("Where - Task 4.2")]
        [Description("Список клиентов с указанием, начиная с какого месяца какого года они стали клиентами")]
        public void Linq4_2()
        {
            foreach (var item in dataSource.Customers)
            {
                Console.WriteLine();
                Console.WriteLine(item.CompanyName);
                Console.WriteLine();
                if (item.Orders.Length == 0)
                {
                    Console.WriteLine("Еще не покупал ничего");
                }
                else
                {
                    Console.WriteLine(item.Orders.Min(s => s.OrderDate));
                }
            }
        }

        [Category("Task 5")]
        [Title("Where - Task 5")]
        [Description("Список отсортированных по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента")]
        public void Linq5()
        {

            var listOfSortedCustomers = dataSource.Customers.Select(x => new
            {
                Customer = x.CompanyName,
                moneyTurnover = x.Orders.Sum(y=>y.Total),
                Date = x.Orders.Length != 0 ? x.Orders.Min(s => s.OrderDate) : new DateTime()
            }).OrderByDescending(x=>x.Date.Year).ThenByDescending(t=>t.Date.Month).ThenByDescending(j=>j.moneyTurnover);

            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine($"{item.Date.Year}--{item.Date.Month}--{item.moneyTurnover}--{item.Customer}");
            }
        }
    }
}

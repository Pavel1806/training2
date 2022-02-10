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

        [Category("Task 6")]
        [Title("Where - Task 6")]
        [Description("Список клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора")]
        public void Linq6()
        {
            int res;
            var listOfSortedCustomers = dataSource.Customers.Where(x =>
                Int32.TryParse(x.PostalCode, out res) == false || x.Region == null || x.Phone.Contains("(") == false);
               
            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine($"{item.PostalCode}--{item.Region}--{item.Phone}--{item.CompanyName}");
            }
        }

        [Category("Task 7")]
        [Title("Where - Task 7")]
        [Description("Сгруппированные все продукты по категориям, внутри – по наличию на складе, внутри последней группы по стоимости")]
        public void Linq7()
        {
            var productGroup = dataSource.Products.GroupBy(p => p.Category);
            int i = 1;
            foreach (var item in productGroup)
            {
                Console.WriteLine();
                Console.WriteLine(item.Key);
                Console.WriteLine();

                if (i != productGroup.Count())
                {
                    foreach (var x in item.OrderBy(x => x.UnitsInStock))
                    {
                        Console.WriteLine($"--{x.ProductName}--{x.UnitsInStock}");
                    }
                }
                else
                {
                    foreach (var x in item.OrderBy(x => x.UnitPrice))
                    {
                        Console.WriteLine($"--{x.ProductName}--{x.UnitPrice}");
                    }
                }

                i++;
            }
        }

        [Category("Task 8")]
        [Title("Where - Task 8")]
        [Description("Сгруппированные товары по группам «дешевые», «средняя цена», «дорогие».")]
        public void Linq8()
        {
            //var productGroup = dataSource.Products.Select(p => p.UnitPrice);

            Console.WriteLine("Дешевые");
            foreach (var item in dataSource.Products.Where(x=>x.UnitPrice > 0 && x.UnitPrice < 30))
            {
                Console.WriteLine($"--{item.UnitPrice} -- {item.ProductName}");
            }

            Console.WriteLine("Средние");
            foreach (var item in dataSource.Products.Where(x => x.UnitPrice >= 30 && x.UnitPrice < 60))
            {
                Console.WriteLine($"--{item.UnitPrice} -- {item.ProductName}");
            }

            Console.WriteLine("Дорогие");
            foreach (var item in dataSource.Products.Where(x => x.UnitPrice >= 60))
            {
                Console.WriteLine($"--{item.UnitPrice} -- {item.ProductName}");
            }

        }

        [Category("Task 9")]
        [Title("Where - Task 9")]
        [Description("Cредняя прибыльность каждого города и средняя интенсивность")]
        public void Linq9()
        {

            var productGroup = dataSource.Customers.GroupBy(x => x.City).Select(v=> new
            {
                City = v.Key,
                Summ = v.Average(c=>c.Orders.Average(x=> x?.Total)),
                NumbOrders =  v.Average(c=>c.Orders.Length) / v?.Count()
            });

            foreach (var item in productGroup)
            {
                var n = item.NumbOrders;
                var s = item.Summ;
                var str1 = string.Format("{0:0.##}", n);
                var str2 = string.Format("{0:0.##}", s);
                Console.WriteLine();
                Console.WriteLine($"{str1}--{str2}--{item.City}");
                Console.WriteLine();
            }
        }
    }
}

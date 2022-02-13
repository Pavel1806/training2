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
using System.Runtime.CompilerServices;
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

            var listOfSortedCustomers = dataSource.Customers.Select(t => new
            {
                Customer = t.CompanyName,
                Suppliers = dataSource.Suppliers.Where(e => e.Country == t.Country).Where(r=> r.City ==t.City)
            });

            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine();
                Console.WriteLine($"Клиент-{item.Customer}");
                Console.WriteLine();
                if (item.Suppliers.Count() != 0)
                {
                    foreach (var x in item.Suppliers)
                    {
                        Console.WriteLine("Поставщики");
                        Console.WriteLine($"{x.SupplierName}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет поставщиков");
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
                Date = x.Orders.OrderBy(s => s.OrderDate).FirstOrDefault()
            }).Where(y=>y.Date != null); // TODO: ИМХО лучше сначала отфильтровать а потом вычислять (это может сократить расходование ресурсов), но так тоже принимается.

            foreach (var item in listOfSortedCustomers)
            {
                Console.WriteLine($"{item.Customer}--{item.Date.OrderDate.Month}.{item.Date.OrderDate.Year}");
            }
        }

        [Category("Task 4")]
        [Title("Where - Task 4.2")]
        [Description("Список клиентов с указанием, начиная с какого месяца какого года они стали клиентами")]
        public void Linq4_2() // TODO: Это не требовалось. Для самопроверки?  //да
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
                Date = x.Orders.OrderBy(s => s?.OrderDate).FirstOrDefault()
            }).Where(s => s.Date != null).OrderByDescending(x=>x.Date.OrderDate.Year)            // переделал
                .ThenByDescending(t=>t.Date.OrderDate.Month)
                .ThenByDescending(j=>j.moneyTurnover)
                .ThenByDescending(b=>b.Customer);

            foreach (var item in listOfSortedCustomers)
            {
                if (item.Date != null)
                {
                    Console.WriteLine($"{item.Date.OrderDate.Year}--{item.Date.OrderDate.Month}--{item.moneyTurnover}--{item.Customer}");
                }
                
            }
        }

        [Category("Task 6")]
        [Title("Where - Task 6")]
        [Description("Список клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора")]
        public void Linq6()
        {
            var listOfSortedCustomers = dataSource.Customers.Where(x =>
                !int.TryParse(x.PostalCode, out _) || x.Region == null || !x.Phone.StartsWith("("));
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

            //var productGroup = dataSource.Products.GroupBy(d => d.Category).Select(r => new
            //{
            //    category = r.Key,
            //    unitsInStock = r.Where(t => t.UnitsInStock > 0), // TODO: Тут ожидалась группировка по признаку есть нас складе, нет на складе, а не сортировка.
            //    unitPrice = r.OrderBy(u => u.UnitPrice),
            

            //});

            var productGroup = dataSource.Products.GroupBy((a) =>  a.Category).Select(f=>new
                {
                    f.Key,
                    product= f.Select(r=>new{r.UnitPrice, r.ProductName, r.UnitsInStock})
                        .GroupBy(e=>e.UnitsInStock > 0)
                        .Select(q=> new
                        {
                            q.Key,
                            product= q.Select(l=> new{l.UnitPrice, l.ProductName, l.UnitsInStock}).OrderBy(x=>x.UnitPrice)
                        })
                        
                });

            foreach (var item in productGroup)
            {
                Console.WriteLine();
                Console.WriteLine("Категория");
                Console.WriteLine($"-{item.Key}");
                Console.WriteLine();
                foreach (var x in item.product)
                {
                    if (x.Key)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"В наличии");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Нет в наличии");
                        Console.WriteLine();
                    }
                    
                    foreach (var y in x.product)
                    {
                        Console.WriteLine($"---{y.ProductName}--{y.UnitsInStock}--{y.UnitPrice}");
                    }
                }
            }

        }

        [Category("Task 8")]
        [Title("Where - Task 8")]
        [Description("Сгруппированные товары по группам «дешевые», «средняя цена», «дорогие».")]
        public void Linq8()
        {
            var productGroup = dataSource.Products.GroupBy(p => new
            {
                cheap = p.UnitPrice > 0 && p.UnitPrice < 30,
                medium = p.UnitPrice >= 30 && p.UnitPrice < 60,
                expensive = p.UnitPrice >= 60

            }).Select(t => new
            {
                t.Key.cheap,
                t.Key.medium,
                t.Key.expensive,
                product = t.Select(y => y.ProductName)
            });

            foreach (var item in productGroup)
            {
                if (item.cheap)
                {
                    Console.WriteLine();
                    Console.WriteLine("Дешевые");
                    foreach (var x in item.product)
                    {
                        Console.WriteLine($"{x}");
                    }
                }
                if (item.medium)
                {
                    Console.WriteLine();
                    Console.WriteLine("Средние");
                    foreach (var x in item.product)
                    {
                        Console.WriteLine($"{x}");
                    }
                }
                if (item.expensive)
                {
                    Console.WriteLine();
                    Console.WriteLine("Дорогие");
                    foreach (var x in item.product)
                    {
                        Console.WriteLine($"{x}");
                    }
                }

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
                NumbOrders =  v.Average(c=>c.Orders.Length) / v.Count()
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


        [Category("Task 10")]
        [Title("Where - Task 10.1")]
        [Description("Cреднегодовая статистика активности клиентов по годам")]
        public void Linq10_1()
        {
            var dateGroups = dataSource.Customers.Select(x => new
            {
                Company = x.CompanyName,
                YearGroups = x.Orders.GroupBy(a=>a.OrderDate.Year).Select(n=> new
                {
                    Year = n.Key,
                    Summ = n.Sum(c=>c.Total),
                })

            });

            foreach (var item in dateGroups)
            {
                Console.WriteLine();
                Console.WriteLine(item.Company);
                Console.WriteLine();
                foreach (var x in item.YearGroups)
                {
                    Console.WriteLine($"{x.Year}--{x.Summ}");
                }
            }
        }

        [Category("Task 10")]
        [Title("Where - Task 10.2")]
        [Description("Cреднегодовая статистика активности клиентов по месяцам за один год(1997)")]
        public void Linq10_2()
        {
            var dateGroups = dataSource.Customers.Select(x => new
            {
                Company = x.CompanyName,
                YearGroups = x.Orders.GroupBy(a => a.OrderDate.Year).Select(n => new
                {
                    Year = n.Key,
                    Summ = n.Sum(c => c.Total),
                    MonthGroups = n.GroupBy(k => k.OrderDate.Month).Select(g => new
                    {
                        Month = g.Key,
                        Summ = g.Sum(d => d.Total)
                    })
                })

            });

            foreach (var item in dateGroups)
            {
                Console.WriteLine();
                Console.WriteLine(item.Company);
                Console.WriteLine();
                foreach (var x in item.YearGroups)
                {
                  foreach (var y in x.MonthGroups)
                    {
                        Console.WriteLine($"{y.Month}.{x.Year}--{y.Summ}");
                    }
                }
            }
        }
        [Category("Task 10")]
        [Title("Where - Task 10.3")]
        [Description("Cреднегодовая статистика активности клиентов по месяцам и годам")]
        public void Linq10_3()
        {
            var dateGroups = dataSource.Customers.Select(x => new
            {
                Company = x.CompanyName,
                YearGroups = x.Orders.GroupBy(a => a.OrderDate.Year).Select(n => new
                {
                    Year = n.Key,
                    Summ = n.Sum(c => c.Total),
                    MonthGroups = n.GroupBy(k => k.OrderDate.Month).Select(g => new
                    {
                        Month = g.Key,
                        Summ = g.Sum(d => d.Total)
                    })
                })

            });

            foreach (var item in dateGroups)
            {
                Console.WriteLine();
                Console.WriteLine(item.Company);
                Console.WriteLine();
                foreach (var x in item.YearGroups)
                {
                    Console.WriteLine();
                    Console.WriteLine($" {x.Year}");
                    Console.WriteLine();

                    foreach (var y in x.MonthGroups)
                    {
                        Console.WriteLine($"  {y.Month}--{y.Summ}");
                    }
                }
            }
        }
    }
}

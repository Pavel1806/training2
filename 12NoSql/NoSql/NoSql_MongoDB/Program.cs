using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSql_MongoDB.Context;
using NoSql_MongoDB.Intarfaces;
using NoSql_MongoDB.Repositories;

namespace NoSql_MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;

            IRepository<Book> bookRepository = new BookRepository(connectionString, "test", "books");

            Book book = new Book { Author = "Tolkien", Name = "Hobbit", Year = 2014, Count = 5 };
            Book book1 = new Book { Author = "Tolkien", Name = "Lord of the rings", Year = 2015, Count = 3 };
            Book book2 = new Book { Name = "Kolobok", Year = 2000, Count = 10 };
            Book book3 = new Book { Name = "Repka", Year = 2000, Count = 11 };
            Book book4 = new Book { Author = "Mihalkov", Name = "Dyadya Stiopa", Year = 2001, Count = 1 };

            book.Genre[0] = "fantasy";
            book1.Genre[0] = "fantasy";
            book2.Genre[0] = "kids";
            book3.Genre[0] = "kids";
            book4.Genre[0] = "kids";

            List<Book> books = new List<Book> { book, book1, book2, book3, book4 };
            bookRepository.Delete();
            bookRepository.Create(books);

            //var books1 = bookRepository.NumberOfInstancesIsMoreThanOne();

            //var books2 = bookRepository.NumberOfInstancesIsMoreThanOneSort();

            //var books3 = bookRepository.NumberOfInstancesIsMoreThanOne_IsNotMoreThree();

            //var books4 = bookRepository.NumberOfInstancesIsMoreThanOne_Count();

            //var books5 = bookRepository.GetBookMaxCount();

            //var authors = bookRepository.GetUniqueAuthor();

            //var nameBooks = bookRepository.GetBooksWithoutAnAuthor();

            //bookRepository.AddFavorityGenre();

            //bookRepository.DeleteTheNumberOf3();

            //var books6 = bookRepository.GetAll();

            //bookRepository.UpdateNumberOfCopies();

            //foreach (var item in books6)
            //{
            //    Console.WriteLine($"{item.Genre[0]}");
            //}

            //foreach (var item in books1)
            //{
            //    Console.WriteLine($"{item.Name}");
            //}

            //Console.WriteLine("-----------------");

            //foreach (var item in books2)
            //{
            //    Console.WriteLine($"{item.Name}");
            //}

            //Console.WriteLine("-----------------");

            //foreach (var item in books3)
            //{
            //    Console.WriteLine($"{item.Name}");
            //}

            //Console.WriteLine("-----------------");

            //Console.WriteLine(books4);

            //Console.WriteLine($"{books5.Name}--{books5.Count}");

            //Console.WriteLine("-----------------");

            //foreach (var item in authors)
            //{
            //    Console.WriteLine($"{item}");
            //}

            //Console.WriteLine("-----------------");

            //foreach (var item in nameBooks)
            //{
            //    Console.WriteLine($"{item}");
            //}
        }
    }
}

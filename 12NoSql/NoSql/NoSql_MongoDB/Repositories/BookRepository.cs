using MongoDB.Bson;
using MongoDB.Driver;
using NoSql_MongoDB.Context;
using NoSql_MongoDB.Intarfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NoSql_MongoDB.Repositories
{
    class BookRepository : IRepository<Book>
    {
        DbContext dbContext;

        IMongoDatabase Database;

        IMongoCollection<Book> Collection;
        public BookRepository(string connectionString, string nameDatabase, string nameCollection)
        {
            dbContext = new DbContext(connectionString);

            Database = dbContext.GetDatabase(nameDatabase);

            Collection = Database.GetCollection<Book>(nameCollection);
        }

        public void Create(List<Book> books)
        {
            Collection.InsertMany(books);
        }

        FilterDefinition<Book> filter_IsMoreThanOne()
        {
            var filter1 = Builders<Book>.Filter.Size("Genre", 5);

            var filter2 = Builders<Book>.Filter.Gt("Count", 2);

            var filter = Builders<Book>.Filter.And(new List<FilterDefinition<Book>> { filter1, filter2 });

            return filter;
        }

        public List<Book> NumberOfInstancesIsMoreThanOne()
        {
            var filter = filter_IsMoreThanOne();

            var books = Collection.Find(filter).ToList();

            return books;
        }

        public List<Book> GetAll()
        {
            var filter2 = Builders<Book>.Filter.Size("Genre", 5);
            
            var books = Collection.Find(filter2).ToList();

            return books;
        }

        public List<Book> NumberOfInstancesIsMoreThanOneSort()
        {
            var filter = filter_IsMoreThanOne();

            var books = Collection.Find(filter).SortBy(e => e.Name).ToList();

            return books;
        }

        public List<Book> NumberOfInstancesIsMoreThanOne_IsNotMoreThree()
        {
            var filter = filter_IsMoreThanOne();

            var books = Collection.Find(filter).Limit(3).ToList();

            return books;
        }
        public int NumberOfInstancesIsMoreThanOne_Count()
        {
            var filter = filter_IsMoreThanOne();

            var books = Collection.Find(filter).ToList().Count;

            return books;
        }
        public Book GetBookMaxCount()
        {
            var filter = Builders<Book>.Filter.Size("Genre", 5);

            var books = Collection.Find(filter).SortByDescending(e=>e.Count).Limit(1).ToList();

            Book book = new Book();

            foreach (var item in books)
            {
                book.Author = item.Author;
                book.Count = item.Count;
                book.Genre = item.Genre;
                book.Name = item.Name;
                book.Year = item.Year;
            }

            return book;
        }

        public List<string> GetUniqueAuthor()
        {
            var uniqueAuthor = Collection.AsQueryable<Book>().Where(e => e.Author != null).Select(e=>e.Author).Distinct().ToList();

            return uniqueAuthor;
        }

        public List<string> GetBooksWithoutAnAuthor()
        {
            var booksWithoutAnAuthor = Collection.AsQueryable<Book>().Where(e => e.Author == null).Select(e => e.Name).Distinct().ToList();

            return booksWithoutAnAuthor;
        }
        public void UpdateNumberOfCopies()
        {
            var result = Collection.UpdateMany(new BsonDocument(), new BsonDocument("$inc", new BsonDocument("Count", 1)));
        }

        public void Delete()
        {
            var filter = new BsonDocument();

            Collection.DeleteMany(filter);  
        }

        public void AddFavorityGenre()
        {
            var filter = Builders<Book>.Filter.All("Genre", new List<string>() {"fantasy"});

            var update = Builders<Book>.Update.AddToSet("Genre", "favority");

            var result = Collection.UpdateMany(filter, update);
        }
        public void DeleteTheNumberOf3()
        {
            var filter = Builders<Book>.Filter.Lt("Count", 3);

            Collection.DeleteMany(filter);
        }
    }
}

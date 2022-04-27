using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NoSql_MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("test");



            var collection = database.GetCollection<Book>("books");

            Book book = new Book { Author = "Tolkien", Name = "Hobbit", Year = 2014, Count = 5, Genre = "fantasy" };
            Book book1 = new Book { Author = "Tolkien", Name = "Lord of the rings", Year = 2015, Count = 3, Genre = "fantasy" };
            Book book2 = new Book { Name = "Kolobok", Year = 2000, Count = 10, Genre = "kids" };
            Book book3 = new Book { Name = "Repka", Year = 2000, Count = 11, Genre = "kids" };
            Book book4 = new Book { Author = "Mihalkov", Name = "Dyadya Stiopa", Year = 2001, Count = 1, Genre = "kids" };

            //collection.InsertOne(book);

            //collection.InsertMany(new[] { book, book1, book2, book3, book4 });

            var filter = new BsonDocument("Count", new BsonDocument("$gt", 2));

            //var filter = new BsonDocument();

            var books = collection.Find(filter).SortBy(e=>e.Name).ToList();

            foreach (var item in books)
            {
                Console.WriteLine($"{item.Name}");
            }

            var countBook = collection.Find(filter).SortBy(e => e.Name).ToList().Count;

            Console.WriteLine(countBook);

            //FindDocs().GetAwaiter().GetResult();
        }

        private static async Task FindDocs()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Book>("books");
            var filter = new BsonDocument();
            var books = await collection.Find(filter).ToListAsync();

            foreach (var doc in books)
            {
                Console.WriteLine("{0} - {1} ({2})", doc.Genre, doc.Name, doc.Count);
            }
        }
    }
}

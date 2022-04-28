using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSql_MongoDB.Context
{
    class DbContext
    {
        MongoClient Client { get; set; }

        public DbContext(string connectionString)
        {
            Client = new MongoClient(connectionString);
        }

        public IMongoDatabase GetDatabase(string nameDatabase)
        {
            IMongoDatabase database = Client.GetDatabase(nameDatabase);

            return database;
        }
    }
}

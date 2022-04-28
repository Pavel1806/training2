using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoSql_MongoDB
{
    //[BsonIgnoreExtraElements]
    class Book
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public string[] Genre { get; set; } = new string[5];
        public int Year { get; set; }
    }
}

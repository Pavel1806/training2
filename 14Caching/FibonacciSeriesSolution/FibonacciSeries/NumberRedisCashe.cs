using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FibonacciSeries
{
    class NumberRedisCashe : INumberCashe
    {
        private ConnectionMultiplexer connectionRedis;

        DataContractSerializer serialiser = new DataContractSerializer(typeof (IEnumerable<int>));

        public NumberRedisCashe(string host)
        {
            connectionRedis = ConnectionMultiplexer.Connect(host);
        }

        public IEnumerable<int> Get(string key)
        {
            var db = connectionRedis.GetDatabase();

            byte [] value = db.StringGet(key);

            if (value == null)
                return null;

            return (IEnumerable<int>)serialiser.ReadObject(new MemoryStream(value));
        }

        public void Set(IEnumerable<int> numbers, string key, DateTimeOffset dateTimeOffset)
        {
            var db = connectionRedis.GetDatabase();

            if(numbers==null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                
                serialiser.WriteObject(stream, numbers);

                var t = dateTimeOffset - DateTimeOffset.Now;

                db.StringSet(key, stream.ToArray(), dateTimeOffset - DateTimeOffset.Now);
            }
        }
    }
}

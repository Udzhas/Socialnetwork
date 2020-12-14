using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2
{
    class MongoConnection
    {
        private MongoConnection()
        {

        }
        public static IMongoDatabase GetDefaultDatabase()
        {
            var connectionString = GetDefaultConnectionString();
            var client = new MongoClient(connectionString);
            return client.GetDatabase(GetDefaultDatabaseName());
        }

        private static string GetDefaultConnectionString()
        {
            return "mongodb://localhost:27017";
        }


        private static string GetDefaultDatabaseName()
        {
            return "test";
        }
    }
}

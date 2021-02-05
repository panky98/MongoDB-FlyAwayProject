using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public static class Session
    {
        private static IMongoDatabase mongoDatabase;

        public static IMongoDatabase MongoDatabase
        {
            get
            {
                if (Session.mongoDatabase == null)
                {
                    var connectionString = "mongodb://localhost/?safe=true";
                    var client = new MongoClient(connectionString);
                    Session.mongoDatabase = client.GetDatabase("flyaway");
                    return Session.mongoDatabase;
                }
                return Session.mongoDatabase;
            }
        }
    }
}

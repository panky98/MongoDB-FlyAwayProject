using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Driver.Core.Servers;
using DataLayer.Models;

namespace DataLayer
{
    public static class DataProvider
    {
        #region Rezervacija

        public static void KreirajKolekcijuRezervacija()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
        }

        public static void KreirajRezervaciju(Rezervacija r)
        {
                IMongoDatabase db = Session.MongoDatabase;
                var collection = db.GetCollection<Rezervacija>("rezervacije");
                collection.InsertOne(r);
        }

        #endregion
    }
}

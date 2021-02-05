using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Driver.Core.Servers;
using DataLayer.Models;
using MongoDB.Bson;

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

        public static Rezervacija VratiRezervacijuPrekoKoda(string kod)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => x.KodRezervacije==kod).FirstOrDefault();
        }

        public static IList<Rezervacija> VratiRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => true).ToList<Rezervacija>();
        }

        public static void ObrisiRezervaciju(String kod)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Rezervacija>("rezervacije").DeleteOne(x => x.KodRezervacije == kod);
        }

        public static void AzurirajRezervaciju(String kod,String newStatus)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Rezervacija>.Filter.Eq(x => x.KodRezervacije,kod);
            var update = Builders<Rezervacija>.Update.Set(x => x.Status, newStatus);


            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter, update);
        }

        #endregion
    }
}

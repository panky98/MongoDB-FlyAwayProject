using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Driver.Core.Servers;
using DataLayer.Models;
using MongoDB.Driver.Builders;

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

        #region Putnik
        public static void KreirajKolekcijuPutnika()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("putnici");
        }

        public static List<Putnik> VratiSvePutnike()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");


            List<Putnik> putnici = new List<Putnik>();

            foreach (Putnik putnik in collection.Find(x => x.ime != "").ToList())
            {
                putnici.Add(putnik);
            }
            return putnici;
        }

        public static List<Putnik> VratiPutnikeRodjenePosle(int godina)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");

            List<Putnik> putnici = new List<Putnik>();

            var filter = Builders<Putnik>.Filter.Gt("godinaRodjenja", 1999);

            var query = Query.GT("godinaRodjenja", 1999);

            /*foreach (Putnik p in collection)
            {
                putnici.Add(p);
            }*/

            return putnici;
        }



        public static void KreirajPutnika(Putnik putnik)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            collection.InsertOne(putnik);
        }


        #endregion
    }
}

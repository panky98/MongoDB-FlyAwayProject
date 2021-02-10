using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Let
    {
        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }*/

        public ObjectId Id {get;set;}

        public string PolazniAerodrom { get; set; }

        public string DolazniAerodrom { get; set; }
        public DateTime DatumLeta { get; set; }

        public int BrojSedista { get; set; }

        public IList<ObjectId> ListaRezervacija { get; set; }


        /* [BsonRepresentation(BsonType.ObjectId)]
         public string AvioKompanija { get; set; }*/
        public ObjectId AvioKompanija { get; set; }

        public Let()
        {
            ListaRezervacija = new List<ObjectId>();
        }

       /* public override bool Equals(object obj)
        {
            if (Id.Equals(((Let)obj).Id))
                return true;
            return false;
        }*/
    }
}

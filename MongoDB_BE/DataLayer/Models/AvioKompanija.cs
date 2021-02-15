using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class AvioKompanija
    {
        /*[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }*/

        public ObjectId Id { get; set; }

        public string Naziv { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string GradPredstavnistva { get; set; }

        public IList<ObjectId> Letovi { get; set; }

        public IList<ObjectId> Komentari { get; set; }

        public AvioKompanija()
        {
            Letovi = new List<ObjectId>();
            Komentari = new List<ObjectId>();
        }
    }
}

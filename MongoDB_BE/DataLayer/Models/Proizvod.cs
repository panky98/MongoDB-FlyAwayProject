using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Proizvod
    {
        public ObjectId Id { get; set; }
        public int cena { get; set; }
        public byte[] SlikaBytes { get; set; }
        public string tip { get; set; }
        public int kolicina { get; set; }
        
    }
}

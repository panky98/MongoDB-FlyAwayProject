using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Putnik
    {
        public ObjectId Id;
        public String ime { get; set; }
        public String prezime { get; set; }
        public int godinaRodjenja { get; set; }
        public Char pol { get; set; }

        public List<ObjectId> rezervacije {get; set;}
    }
}

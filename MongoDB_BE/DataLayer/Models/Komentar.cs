using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Komentar
    {
        public ObjectId Id;
        public String ime { get; set; }
        public String prezime { get; set; }
        public String tekstKomentara { get; set; }
        public int brZvezdica { get; set; }
    }
}

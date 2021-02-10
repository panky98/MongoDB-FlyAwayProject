using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class PutnikDTO
    {
        public ObjectId Id { get; set; }
        public String jmbg { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public int godinaRodjenja { get; set; }
        public Char pol { get; set; }
        public List<Rezervacija> rezervacije { get; set; }
    }
}

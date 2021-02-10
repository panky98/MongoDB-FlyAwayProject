using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Kofer
    {
        public ObjectId Id { get; set; }
        public String tip { get; set; }
        public int tezina { get; set; }
        public bool rucniPrtljag { get; set; }
        public ObjectId Rezervacija { get; set; }

    }
}

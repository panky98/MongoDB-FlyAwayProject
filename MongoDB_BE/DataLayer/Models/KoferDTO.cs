using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class KoferDTO
    {
        public string Id { get; set; }
        public String tip { get; set; }
        public int tezina { get; set; }
    }
}

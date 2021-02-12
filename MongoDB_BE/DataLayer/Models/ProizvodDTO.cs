using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
namespace DataLayer.Models
{
    public class ProizvodDTO
    {
        public string Id { get; set; }
        public int cena { get; set; }
        public string SlikaBytesBase64 { get; set; }
        public string tip { get; set; }
        public string naziv { get; set; }

        public int  kolicina { get; set; }
    }
}

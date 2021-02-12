using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Rezervacija
    {
        public ObjectId Id { get; set; }
        public int BrojSedista { get; set; }
        public byte[] PasosBytes { get; set; }
        public byte[] CovidTestBytes { get; set; }
        public string Status { get; set; }
        public string KodRezervacije { get; set; }
        public IList<ObjectId> ListaProizvoda { get; set; }
        public ObjectId Putnik { get; set; }
        public ObjectId Let { get; set; }
        public ObjectId Kofer { get; set; }

        public static explicit operator Rezervacija(ObjectId v)
        {
            throw new NotImplementedException();
        }
    }
}

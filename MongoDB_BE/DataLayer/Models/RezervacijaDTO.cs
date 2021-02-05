﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class RezervacijaDTO
    {
        public ObjectId Id { get; set; }
        public int BrojSedista { get; set; }
        public string PasosBytesBase64 { get; set; }
        public string CovidTestBytesBase64 { get; set; }
        public string Status { get; set; }
        public string KodRezervacije { get; set; }
        public IList<ObjectId> ListaProizvoda { get; set; }
        public IList<ObjectId> ListaKofera { get; set; }
        public ObjectId Putnik { get; set; }
        public ObjectId Let { get; set; }
    }
}

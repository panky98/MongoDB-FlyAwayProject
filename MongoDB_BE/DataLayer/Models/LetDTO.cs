using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class LetDTO
    {
        public string Id { get; set; }

        public string PolazniAerodrom { get; set; }

        public string DolazniAerodrom { get; set; }
        public DateTime DatumLeta { get; set; }

        public int BrojSedista { get; set; }

        public IList<string> ListaRezervacija { get; set; }

        public string AvioKompanija { get; set; }

        public LetDTO()
        {
            ListaRezervacija = new List<string>();
        }
    }
}

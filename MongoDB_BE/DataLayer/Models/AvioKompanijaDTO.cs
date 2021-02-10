using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class AvioKompanijaDTO
    {
        public string Id { get; set; }

        public string Naziv { get; set; }

        public int GodinaOsnivanja { get; set; }

        public string GradPredstavnistva { get; set; }

        public IList<string> Letovi { get; set; }

        public IList<string> Komentari { get; set; }

        public AvioKompanijaDTO()
        {
            Letovi = new List<string>();
            Komentari = new List<string>();
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IzdavanjeFaktura.Models.Models
{
    public class Stavka
    {
        public int StavkaID { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public string Opis { get; set; }

        public List<FakturaStavka> Fakture { get; set; }
    }
}
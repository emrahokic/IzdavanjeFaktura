using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.Models
{
    public class Faktura
    {
        public int FakturaID { get; set; }
        public string BrojFakture { get; set; }
        public DateTime DatumFakture { get; set; }
        public DateTime DatumPlacanjaFakture { get; set; }
        public double UkupnaCijenaBezPoreza { get; set; }
        public double UkupnaCijenaSaPorezom { get; set; }
        public string StvarateljRacunaID { get; set; }
        public ApplicationUser StvarateljRacuna { get; set; }
        public string NazivPrimatelja { get; set; }
        public bool Placeno { get; set; }

        public int PorezID { get; set; }
        public Porez Porez { get; set; }
        public List<FakturaStavka> Stavke { get; set; }
    }
}
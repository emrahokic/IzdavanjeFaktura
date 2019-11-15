using IzdavanjeFaktura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.ViewModels
{
    public class FakturaVM
    {
        public int FakturaID { get; set; }
        public string BrojFakture { get; set; }
        public DateTime DatumFakture { get; set; }
        public DateTime DatumPlacanjaFakture { get; set; }
        public double UkupnaCijenaBezPoreza { get; set; }
        public double UkupnaCijenaSaPorezom { get; set; }
        public string NazivPrimatelja { get; set; }
        public bool Placeno { get; set; }
        public string Porez { get; set; }

        public string StvarateljRacuna { get; set; }
        public List<StavkaVM> Stavke { get; set; }

        public FakturaVM()
        {
            Stavke = new List<StavkaVM>();
        }
    }
}
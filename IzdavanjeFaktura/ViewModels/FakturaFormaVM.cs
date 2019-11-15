using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IzdavanjeFaktura.ViewModels
{
    public class FakturaFormaVM
    {
        //forma1
        [Required]
        public string BrojFakture { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumFakture { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumPlacanjaFakture { get; set; }
        //public double UkupnaCijenaBezPoreza { get; set; }
        //public double UkupnaCijenaSaPorezom { get; set; }
        public string NazivPrimatelja { get; set; }
        public int PorezID { get; set; }
        [Required]
        public List<Row> SelektovaneStavke { get; set; }

        [DisplayName("Porez")]
        public int PDVID { get; set; }
        public int i { get; set; }
        public List<SelectListItem> PDV { get; set; }

        public bool Placeno { get; set; }
        public class Row
        {
            public int StavkaID { get; set; }
            public double Cijena { get; set; }
            public string Naziv { get; set; }
            public int Kolicina { get; set; }
        }


        //forma2
        [DisplayName("Stavke")]
        public int StavkaID { get; set; }
        public List<SelectListItem> Stavke { get; set; }
        //public Row Stavka { get; set; }
        public FakturaFormaVM()
        {
            PDV = new List<SelectListItem>();
            Stavke = new List<SelectListItem>();
            SelektovaneStavke = new List<Row>();
        }
    }
}

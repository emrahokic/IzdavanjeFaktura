using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.Models.Models
{
    public class Porez
    {
        public int PorezID { get; set; }
        public string Naziv { get; set; }
        public double Iznos { get; set; }
    }
}
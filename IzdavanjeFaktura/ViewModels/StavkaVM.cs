using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IzdavanjeFaktura.ViewModels
{
    public class StavkaVM
    {
        public int StavkaID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public double Cijena { get; set; }  
        public double JedinicnaCijena { get; set; }  
        [Required]
        public int Kolicina { get; set; }
        [Required]
        public string Opis{ get; set; }
    }
}
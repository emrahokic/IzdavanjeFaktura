namespace IzdavanjeFaktura.Models.Models
{
    public class FakturaStavka
    {
        public int FakturaStavkaID { get; set; }

        public int FakturaID { get; set; }
        public Faktura Faktura { get; set; }

        public int StavkaID { get; set; }
        public Stavka Stavka { get; set; }

        public int Kolicina { get; set; }
        public double UkupnaCijenaBezPoreza { get; set; }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IzdavanjeFaktura.ViewModels;
using IzdavanjeFaktura.Services;

namespace IzdavanjeFaktura.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestKalkulacijaPoreza_Prolaz()
        {
            FakturaService fakturaService = new FakturaService();
            var iznos_pdv17 = fakturaService._izracunavanjeCijeneSaPDV(20, 17);
            var iznos_pdv25 = fakturaService._izracunavanjeCijeneSaPDV(20, 25);
            var ocekivanIznos_1 = 23.4;
            var ocekivanIznos_2 = 25;
            Assert.AreEqual(ocekivanIznos_1, iznos_pdv17);
            Assert.AreEqual(ocekivanIznos_2, iznos_pdv25);

        }



        [TestMethod]
        public void TestKalkulacijaUkupnogIznosa()
        {
            FakturaService fakturaService = new FakturaService();
            FakturaFormaVM.Row stavka1 = new FakturaFormaVM.Row()
            {
                Cijena = 10,
                Kolicina = 2
            }; //20
            FakturaFormaVM.Row stavka2 = new FakturaFormaVM.Row()
            {
                Cijena = 5,
                Kolicina = 3
            }; //15
            FakturaFormaVM.Row stavka3 = new FakturaFormaVM.Row()
            {
                Cijena = 11.20,
                Kolicina = 4
            }; //44.80
            FakturaFormaVM.Row stavka4 = new FakturaFormaVM.Row()
            {
                Cijena = 0.55,
                Kolicina = 3
            }; //1.65
              

            FakturaFormaVM faktura = new FakturaFormaVM();
            faktura.SelektovaneStavke = new System.Collections.Generic.List<FakturaFormaVM.Row>();
            faktura.SelektovaneStavke.Add(stavka1);
            faktura.SelektovaneStavke.Add(stavka2);
            faktura.SelektovaneStavke.Add(stavka3);
            faktura.SelektovaneStavke.Add(stavka4);

            var iznos = fakturaService._izracunavanjeUkupneCijene(faktura.SelektovaneStavke);
            var ocekivanIznos = 81.45;

            Assert.AreEqual(ocekivanIznos, iznos);

        }

    }
}

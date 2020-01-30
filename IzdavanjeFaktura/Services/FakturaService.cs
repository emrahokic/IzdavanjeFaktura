using IzdavanjeFaktura.Repository;
using IzdavanjeFaktura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.Composition;
using IzdavanjeFaktura.MEF;
using System.ComponentModel.Composition.Hosting;
using IzdavanjeFaktura.Models.Models;

namespace IzdavanjeFaktura.Services
{
    public class FakturaService : IFakturaService
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private CompositionContainer _container;

        [Import(typeof(IPorezKalkulator))]
        private IPorezKalkulator _porezKalkulator;

        public FakturaService()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(FakturaService).Assembly));
            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
        public Faktura GetById(int id)
        {
            return _db.Faktura.Include(y=>y.Stavke).Include(y=>y.Porez).Include(y=>y.StvarateljRacuna).Where(x=>x.FakturaID == id).FirstOrDefault();
        }

        public List<Faktura> GetAll()
        {
            return _db.Faktura.ToList();
        }

        public List<FakturaVM> GetAll_VM()
        {
            List<FakturaVM> fakture = _db.Faktura.Include(y => y.Porez).Select(x => new FakturaVM()
            {
                BrojFakture = x.BrojFakture,
                DatumFakture = x.DatumFakture,
                DatumPlacanjaFakture = x.DatumPlacanjaFakture,
                NazivPrimatelja = x.NazivPrimatelja,
                Placeno = x.Placeno,
                Porez = x.Porez.Naziv,
                UkupnaCijenaBezPoreza = x.UkupnaCijenaBezPoreza,
                UkupnaCijenaSaPorezom = x.UkupnaCijenaSaPorezom,
                FakturaID = x.FakturaID
            }).ToList();

            return fakture;
        }

        public Faktura Insert(FakturaFormaVM model,string id)
        {
            string _stvarateljRacunaID =id;
            double PDV = _db.Porez.Where(x => x.PorezID == model.PDVID).Select(x => x.Iznos).FirstOrDefault();
            Faktura faktura = new Faktura()
            {
                BrojFakture = model.BrojFakture,
                DatumFakture = model.DatumFakture,
                DatumPlacanjaFakture = model.DatumPlacanjaFakture,
                NazivPrimatelja = model.NazivPrimatelja,
                PorezID = model.PDVID,
                Placeno = model.Placeno,
                StvarateljRacunaID = _stvarateljRacunaID,
                UkupnaCijenaBezPoreza = _izracunavanjeUkupneCijene(model.SelektovaneStavke),
                UkupnaCijenaSaPorezom = _izracunavanjeCijeneSaPDV(_izracunavanjeUkupneCijene(model.SelektovaneStavke), PDV)

            };

            faktura.Stavke = new List<FakturaStavka>();
            foreach (var item in model.SelektovaneStavke)
            {
                faktura.Stavke.Add(new FakturaStavka()
                {
                    Kolicina = item.Kolicina,
                    StavkaID = _db.Stavka.Where(x=>x.Naziv.Equals(item.Naziv) && x.StavkaID == item.StavkaID).Select(x=>x.StavkaID).FirstOrDefault(),
                    UkupnaCijenaBezPoreza = item.Kolicina * _db.Stavka.Where(x=>x.StavkaID == item.StavkaID).Select(x=>x.Cijena).FirstOrDefault()
                });
            }

            _db.Faktura.Add(faktura);
            _db.SaveChanges();
            return faktura;
        }

        public double _izracunavanjeCijeneSaPDV(double iznos, double pdv)
        {
            
            return  new FakturaService()._porezKalkulator.Izracunaj(iznos, pdv);
        }

        public double _izracunavanjeUkupneCijene(List<FakturaFormaVM.Row> selektovaneStavke)
        {
            double suma = 0;
            foreach (var item in selektovaneStavke)
            {
                suma += (double)item.Kolicina * item.Cijena;
            }
            return suma;
        }
    }
}
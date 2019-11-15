using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IzdavanjeFaktura.Models;
using IzdavanjeFaktura.ViewModels;

namespace IzdavanjeFaktura.Services
{
    public class StavkaService : IStavkaService
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Delete(int id)
        {
            _db.Stavka.Remove(_db.Stavka.Where(x => x.StavkaID == id).FirstOrDefault());
            _db.SaveChanges();
        }

        public Stavka Edit(int id, StavkaVM stavka)
        {
            Stavka modelU = _db.Stavka.Where(x => x.StavkaID == id).FirstOrDefault();

            modelU.Opis = stavka.Opis;
            modelU.Cijena = stavka.Cijena;
            modelU.Naziv = stavka.Naziv;
            _db.SaveChanges();

            return modelU;
        }

        public List<Stavka> GetAll()
        {
            return _db.Stavka.ToList();
        }

        public List<StavkaVM> GetAll_VM()
        {
            return _db.Stavka.Select(x => new StavkaVM()
            {
                Cijena = x.Cijena,
                Naziv = x.Naziv,
                StavkaID = x.StavkaID
            }).ToList();
        }

        public Stavka GetById(int id)
        {
            return _db.Stavka.Where(x=>x.StavkaID == id).FirstOrDefault();
        }

        public Stavka Insert(StavkaVM model)
        {
            Stavka insert = new Stavka()
            {
                Opis = model.Opis,
                Naziv = model.Naziv,
                Cijena = model.Cijena
            };
            _db.Stavka.Add(insert);
            _db.SaveChanges();
            return insert;
        }
    }
}
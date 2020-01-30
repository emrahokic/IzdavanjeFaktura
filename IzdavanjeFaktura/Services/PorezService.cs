using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IzdavanjeFaktura.Models.Models;
using IzdavanjeFaktura.Repository;

namespace IzdavanjeFaktura.Services
{
    public class PorezService : IPorezService
    {
        private readonly ApplicationDbContext _db= new ApplicationDbContext();

        IRepository<Porez> repositoryPorez;

        public PorezService(IRepository<Porez> repository)
        {
            this.repositoryPorez = repository;
        }
        public IEnumerable<Porez> GetAll()
        {
            //return _db.Porez.ToList();
            return repositoryPorez.GetAll().Where(x=>x.Iznos>16).ToList();
        }

        public Porez GetById(int id)
        {
            return _db.Porez.Where(x=>x.PorezID == id).FirstOrDefault();
        }
    }
}
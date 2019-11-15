using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IzdavanjeFaktura.Models;

namespace IzdavanjeFaktura.Services
{
    public class PorezService : IPorezService
    {
        private readonly ApplicationDbContext _db= new ApplicationDbContext();
        public List<Porez> GetAll()
        {
            return _db.Porez.ToList();
        }

        public Porez GetById(int id)
        {
            return _db.Porez.Where(x=>x.PorezID == id).FirstOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzdavanjeFaktura.Repository
{

    public interface IRepository<TEntity> {


        void Add(TEntity obj);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();


    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        ApplicationDbContext _db;
        DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<TEntity>();

        }
        public void Add(TEntity obj)
        {
            dbSet.Add(obj);
            _db.SaveChanges();

        }

        public TEntity Get(int id)
        {

            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }
    }
}

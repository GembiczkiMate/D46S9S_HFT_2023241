using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected OrderDB db;
        public Repository(OrderDB db)
        {
            this.db = db;
        }

        public void Create(T item)
        {
           db.Set<T>().Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Set<T>().Remove(Read(id));
            db.SaveChanges();
        }

       
       

        public IQueryable<T> ReadAll()
        {
            return db.Set<T>();
        }

        public abstract T Read(int id);

        public abstract void Update(T item);
        
    }
}

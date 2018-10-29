using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMS.Interfaces;

namespace TMS.Data.Repositories
{
    public class BasicRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> db;
        private readonly TMSDbContext context;
        public BasicRepository(TMSDbContext context)
        {
            this.context = context;
            db = this.context.Set<T>();
        }

        public void Create(T item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Find(id);
            if (item != null)
                db.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return db;
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return db.Where(predicate);
        }

        public T GetItem(int id)
        {
            return db.Find(id);
        }

        public void Update(T item)
        {
            db.Update(item);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
               }
            }
            this.disposed = true;
        }

      
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Data.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly DbSet<Task> db;
        private readonly TMSDbContext context;

        public TaskRepository(TMSDbContext context)
        {
            this.context = context;
            db = this.context.Set<Task>();
        }

        public void Create(Task item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Find(id);
            if (item != null)
            {
                db.Remove(item);
            }
        }

        public IEnumerable<Task> Filter(Expression<Func<Task, bool>> predicate)
        {
            return GetAllQuery()
            .Where(predicate)
            .ToList();
        }

        public IEnumerable<Task> GetAll()
        {
            return GetAllQuery()
               .ToList();
        }

        public Task GetItem(int id)
        {
            return GetAllQuery()
            .FirstOrDefault(t => t.Id == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Task item)
        {
            db.Update(item); // toDO
        }

        private IQueryable<Task> GetAllQuery()
        {
            return db
                .Include(v => v.Moderators);
               // .Include(v => v.Viewers);

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

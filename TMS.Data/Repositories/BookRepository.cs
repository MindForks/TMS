using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Data
{
    public class BookRepository : IRepository<Book>
    {
        private DbSet<Book> db;

        public BookRepository(TMSDbContext context)
        {
            db = context.Set<Book>();
        }

        public void Create(Book item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Book item = db.Find(id);
            if (item != null)
                db.Remove(item);
        }

        public IEnumerable<Book> GetAll()
        {
            return db; // in the future we need to include conntected tables
        }

        public Book GetItem(int id)
        {
            return db.Find(id);
        }

        public void Update(Book item)
        {
            db.Update(item);
        }
    }
}

using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TMSDbContext _dbContext;

        public UnitOfWork(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
            Books = new BookRepository(dbContext);
        }

        #region Repositories
        public IRepository<Book> Books { get; }
        #endregion

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

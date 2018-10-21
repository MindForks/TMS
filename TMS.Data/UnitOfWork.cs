using TMS.Interfaces;

namespace TMS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TMSDbContext _dbContext;

        public UnitOfWork(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Repositories
        #endregion

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

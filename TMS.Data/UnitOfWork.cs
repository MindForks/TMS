using TMS.Interfaces;
using TMS.Entities;
using TMS.Data.Repositories;

namespace TMS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TMSDbContext _dbContext;

        public UnitOfWork(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
            NotificationTypes = new BasicRepository<NotificationType>(_dbContext);
        }

        #region Repositories
        public IRepository<NotificationType> NotificationTypes { get;set;}
        #endregion

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

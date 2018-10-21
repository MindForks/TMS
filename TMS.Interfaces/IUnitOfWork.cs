using TMS.Entities;
namespace TMS.Interfaces
{
   public interface IUnitOfWork
    {
        #region IRepositories
        IRepository<NotificationType> NotificationTypes { get; }
        #endregion
        void Save();
    }
}

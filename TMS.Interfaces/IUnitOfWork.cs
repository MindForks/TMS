using System.Threading.Tasks;
using TMS.Entities;

namespace TMS.Interfaces
{
   public interface IUnitOfWork
    {
        IRepository<Book> Books { get; }

        void Save();
    }
}

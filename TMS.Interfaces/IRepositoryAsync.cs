using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Interfaces
{
    public interface IRepositoryAsync<T>  where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetItemAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}

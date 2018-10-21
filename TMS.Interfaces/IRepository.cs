using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TMS.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
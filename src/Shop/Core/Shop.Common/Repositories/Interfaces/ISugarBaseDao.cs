using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Repositories.Interfaces
{
    public interface ISugarBaseDao<T> where T : class, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, int pageNo, int pageSize);

        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);

        Task<int> AddAsync(T entity);

        Task<int> AddAsync(List<T> entities);

        Task<int> DeleteAsync(T entity);

        Task<int> DeleteAsync(List<T> entities);

        Task<int> DeleteAsync(Expression<Func<T, bool>> expression);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateAsync(List<T> entities);
    }
}

using Shop.Common.Repositories.Interfaces;
using Shop.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Services.Implements
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        protected readonly ISugarBaseDao<T> _sugarBaseDao;

        public BaseService(ISugarBaseDao<T> sugarBaseDao)
        {
            _sugarBaseDao = sugarBaseDao;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarBaseDao.GetAsync(expression);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarBaseDao.GetListAsync(expression);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, int pageNo, int pageSize)
        {
            return await _sugarBaseDao.GetListAsync(expression, pageNo, pageSize);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarBaseDao.GetCountAsync(expression);
        }

        public async Task<int> AddAsync(T entity)
        {
            return await _sugarBaseDao.AddAsync(entity);
        }

        public async Task<int> AddAsync(List<T> entities)
        {
            return await _sugarBaseDao.AddAsync(entities);
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await _sugarBaseDao.DeleteAsync(entity);
        }

        public async Task<int> DeleteAsync(List<T> entities)
        {
            return await _sugarBaseDao.DeleteAsync(entities);
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarBaseDao.DeleteAsync(expression);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await _sugarBaseDao.UpdateAsync(entity);
        }

        public async Task<int> UpdateAsync(List<T> entities)
        {
            return await _sugarBaseDao.UpdateAsync(entities);
        }
    }
}

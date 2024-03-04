using Microsoft.Extensions.Configuration;
using Hotel.Dao.interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dao.implements
{
    public class SugarBaseDao<T> : ISugarBaseDao<T> where T : class, new()
    {
        protected readonly SqlSugarClientFactory _sqlSugarClientFactory;
        protected readonly ISqlSugarClient _sugarSqlClient;

        public SugarBaseDao(SqlSugarClientFactory sqlSugarClientFactory)
        {
            _sqlSugarClientFactory = sqlSugarClientFactory;
            _sugarSqlClient = _sqlSugarClientFactory.Get("ConnectionStrings:ShopDb");
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarSqlClient.Queryable<T>().Where(expression).FirstAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarSqlClient.Queryable<T>().Where(expression).ToListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, int pageNo, int pageSize)
        {
            return await _sugarSqlClient.Queryable<T>().Where(expression).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarSqlClient.Queryable<T>().Where(expression).CountAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            return await _sugarSqlClient.Insertable<T>(entity).ExecuteCommandAsync();
        }

        public async Task<int> AddAsync(List<T> entities)
        {
            return await _sugarSqlClient.Insertable<T>(entities).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await _sugarSqlClient.Deleteable<T>(entity).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(List<T> entities)
        {
            return await _sugarSqlClient.Deleteable<T>(entities).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            return await _sugarSqlClient.Deleteable<T>(expression).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await _sugarSqlClient.Updateable<T>(entity).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(List<T> entities)
        {
            return await _sugarSqlClient.Updateable<T>(entities).ExecuteCommandAsync();
        }
    }
}

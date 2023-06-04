using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.IRepositories;
using Wyb.Study.Requests.FunctionItem;

namespace Wyb.Study.Repositories
{
    public class FunctionItemRepository : IFunctionItemRepository
    {
        private readonly IConfiguration _configuration;
        public FunctionItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateAsync(FunctionItem item)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("INSERT INTO function_item(function_name,function_url,create_time,update_time) VALUES(@FunctionName,@FunctionUrl,@CreateTime,@UpdateTime);", new
                {
                    FunctionName = item.FunctionName,
                    FunctionUrl = item.FunctionUrl,
                    CreateTime = item.CreateTime,
                    UpdateTime = item.UpdateTime
                });
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("DELETE FROM function_item Where id=@Id", new
                {
                    Id = id
                });
            }
        }

        public async Task<int> UpdateAsync(FunctionItem item)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("UPDATE function_item set function_name=@FunctionName,@function_url=@FunctionUrl,update_time=@UpdateTime WHERE id=@Id", new
                {
                    FunctionName = item.FunctionName,
                    FunctionUrl = item.FunctionUrl,
                    UpdateTime = item.UpdateTime
                });
            }
        }

        public async Task<FunctionItem> GetAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,function_name AS FunctionName,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM function_item WHERE id=@Id", new { Id = id });
                if (data != null)
                {
                    return new FunctionItem()
                    {
                        Id = data.Id,
                        FunctionName = data.FunctionName,
                        FunctionUrl = data.FunctionUrl,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }

        public async Task<List<FunctionItem>> GetListAsync(GetFunctionItemListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,function_name AS FunctionName,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM function_item WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.FunctionName))
                {
                    sql += " AND function_name like '%@FunctionName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.FunctionUrl))
                {
                    sql += " AND function_url=@FunctionUrl";
                }
                sql += $" LIMIT {(request.PageIndex - 1) * request.PageSize},{request.PageSize}";
                var data = await conn.QueryAsync(sql, new { FunctionName = request.FunctionName, FunctionUrl = request.FunctionUrl, request.PageIndex, request.PageSize });
                if (data != null)
                {
                    return data.Select(p => new FunctionItem()
                    {
                        Id = p.Id,
                        FunctionName = p.FunctionName,
                        FunctionUrl = p.FunctionUrl,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                return new List<FunctionItem>();
            }
        }

        public async Task<long> GetCountAsync(GetFunctionItemListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT COUNT(Id) FROM function_item WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.FunctionName))
                {
                    sql += " AND function_name like '%@FunctionName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.FunctionUrl))
                {
                    sql += " AND function_url=@FunctionUrl";
                }
                return await conn.ExecuteScalarAsync<int>(sql, new { FunctionName = request.FunctionName, FunctionUrl = request.FunctionUrl });
            }
        }

        public async Task<FunctionItem> GetAsync(string functionName, string functionUrl)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,function_name AS FunctionName,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM function_item WHERE function_name=@FunctionName AND function_url=@FunctionUrl", new { FunctionName = functionName, FunctionUrl = functionUrl });
                if (data != null)
                {
                    return new FunctionItem()
                    {
                        Id = data.Id,
                        FunctionName = data.FunctionName,
                        FunctionUrl = data.FunctionUrl,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }
    }
}

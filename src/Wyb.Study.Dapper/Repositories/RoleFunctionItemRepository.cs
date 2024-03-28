using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Requests.RoleFunctionItem;
using Wyb.Study.Domain.DbEntities;
using Wyb.Study.Domain.IRepositories;

namespace Wyb.Study.Dapper.Repositories
{
    public class RoleFunctionItemRepository : IRoleFunctionItemRepository
    {
        private readonly IConfiguration _configuration;
        public RoleFunctionItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateAsync(RoleFunctionItem roleFunctionItem)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("INSERT INTO role_function_item(role_code,function_url,create_time,update_time) VALUES(@RoleCode,@FunctionUrl,@CreateTime,@UpdateTime);", new
                {
                    roleFunctionItem.RoleCode,
                    roleFunctionItem.FunctionUrl,
                    roleFunctionItem.CreateTime,
                    roleFunctionItem.UpdateTime
                });
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("DELETE FROM role_function_item Where id=@Id", new
                {
                    Id = id
                });
            }
        }

        public async Task<int> UpdateAsync(RoleFunctionItem roleFunctionItem)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("UPDATE role_function_item set role_code=@RoleCode,function_url=@FunctionUrl,update_time=@UpdateTime WHERE id=@Id", new
                {
                    roleFunctionItem.RoleCode,
                    roleFunctionItem.FunctionUrl,
                    roleFunctionItem.UpdateTime
                });
            }
        }

        public async Task<RoleFunctionItem> GetAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,role_code AS RoleCode,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM role_function_item WHERE id=@Id", new { Id = id });
                if (data != null)
                {
                    return new RoleFunctionItem()
                    {
                        Id = data.Id,
                        RoleCode = data.RoleCode,
                        FunctionUrl = data.FunctionUrl,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }

        public async Task<List<RoleFunctionItem>> GetListAsync(GetRoleFunctionItemListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,role_code AS RoleCode,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM role_function_item WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                sql += $" LIMIT {(request.PageIndex - 1) * request.PageSize},{request.PageSize}";
                var data = await conn.QueryAsync(sql, new { request.RoleCode, request.PageIndex, request.PageSize });
                if (data != null)
                {
                    return data.Select(p => new RoleFunctionItem()
                    {
                        Id = p.Id,
                        RoleCode = p.RoleCode,
                        FunctionUrl = p.FunctionUrl,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                return new List<RoleFunctionItem>();
            }
        }

        public async Task<long> GetCountAsync(GetRoleFunctionItemListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT COUNT(Id) FROM role_function_item WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                return await conn.ExecuteScalarAsync<int>(sql, new { request.RoleCode });
            }
        }

        public async Task<RoleFunctionItem> GetAsync(string roleCode, string functionUrl)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,role_code AS RoleCode,function_url AS FunctionUrl,create_time AS CreateTime,update_time AS UpdateTime FROM role_function_item WHERE role_code=@RoleCode AND function_url=@FunctionUrl", new { RoleCode = roleCode, FunctionUrl = functionUrl });
                if (data != null)
                {
                    return new RoleFunctionItem()
                    {
                        Id = data.Id,
                        RoleCode = data.RoleCode,
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

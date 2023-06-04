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
using Wyb.Study.Dtos.Users;
using Wyb.Study.IRepositories;
using Wyb.Study.Requests.Role;

namespace Wyb.Study.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _configuration;
        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateAsync(Role role)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("INSERT INTO Role(role_name,role_code,create_time,update_time) VALUES(@RoleName,@RoleCode,@CreateTime,@UpdateTime);", new
                {
                    RoleName = role.RoleName,
                    RoleCode = role.RoleCode,
                    CreateTime = role.CreateTime,
                    UpdateTime = role.UpdateTime
                });
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("DELETE FROM role Where id=@Id", new
                {
                    Id = id
                });
            }
        }

        public async Task<int> UpdateAsync(Role role)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("UPDATE role set role_name=@RoleName,update_time=@UpdateTime WHERE id=@Id", new
                {
                    RoleName = role.RoleName,
                    UpdateTime = role.UpdateTime
                });
            }
        }

        public async Task<Role> GetAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,role_name AS RoleName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE id=@Id", new { Id = id });
                if (data != null)
                {
                    return new Role()
                    {
                        Id = data.Id,
                        RoleName = data.RoleName,
                        RoleCode = data.RoleCode,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }

        public async Task<List<Role>> GetListAsync(GetRoleListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,role_name AS RoleName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.RoleName))
                {
                    sql += " AND role_name like '%@RoleName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                sql += $" LIMIT {(request.PageIndex - 1) * request.PageSize},{request.PageSize}";
                var data = await conn.QueryAsync(sql, new { RoleName = request.RoleName, RoleCode = request.RoleCode, request.PageIndex, request.PageSize });
                if (data != null)
                {
                    return data.Select(p => new Role()
                    {
                        Id = p.Id,
                        RoleName = p.RoleName,
                        RoleCode = p.RoleCode,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                return new List<Role>();
            }
        }

        public async Task<long> GetCountAsync(GetRoleListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT COUNT(Id) FROM role WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.RoleName))
                {
                    sql += " AND role_name like '%@RoleName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                return await conn.ExecuteScalarAsync<int>(sql, new { RoleName = request.RoleName, RoleCode = request.RoleCode });
            }
        }

        public async Task<Role> GetByRoleCodeAsync(string roleCode)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,role_name AS RoleName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE role_code=@RoleCode";
                return await conn.QueryFirstOrDefaultAsync(sql, new { RoleCode = roleCode });
            }
        }
    }
}

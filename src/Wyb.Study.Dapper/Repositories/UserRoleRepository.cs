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
using Wyb.Study.Requests.UserRole;

namespace Wyb.Study.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IConfiguration _configuration;
        public UserRoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateAsync(UserRole userRole)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("INSERT INTO user_role(user_name,role_code,create_time,update_time) VALUES(@RoleName,@RoleCode,@CreateTime,@UpdateTime);", new
                {
                    UserName = userRole.UserName,
                    RoleCode = userRole.RoleCode,
                    CreateTime = userRole.CreateTime,
                    UpdateTime = userRole.UpdateTime
                });
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("DELETE FROM user_role Where id=@Id", new
                {
                    Id = id
                });
            }
        }

        public async Task<int> UpdateAsync(UserRole userRole)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("UPDATE role set user_name=@UserName,role_code=@RoleCode,update_time=@UpdateTime WHERE id=@Id", new
                {
                    UserName = userRole.UserName,
                    RoleCode = userRole.RoleCode,
                    UpdateTime = userRole.UpdateTime
                });
            }
        }

        public async Task<UserRole> GetAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,user_name AS UserName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE id=@Id", new { Id = id });
                if (data != null)
                {
                    return new UserRole()
                    {
                        Id = data.Id,
                        UserName = data.UserName,
                        RoleCode = data.RoleCode,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }

        public async Task<List<UserRole>> GetListAsync(GetUserRoleListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,user_name AS UserName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    sql += " AND user_name like '%@UserName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                sql += $" LIMIT {(request.PageIndex - 1) * request.PageSize},{request.PageSize}";
                var data = await conn.QueryAsync(sql, new { RoleName = request.UserName, RoleCode = request.RoleCode, request.PageIndex, request.PageSize });
                if (data != null)
                {
                    return data.Select(p => new UserRole()
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        RoleCode = p.RoleCode,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                return new List<UserRole>();
            }
        }

        public async Task<long> GetCountAsync(GetUserRoleListRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT COUNT(Id) FROM role WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    sql += " AND role_name like '%@RoleName%'";
                }
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    sql += " AND role_code=@RoleCode";
                }
                return await conn.ExecuteScalarAsync<int>(sql, new { UserName = request.UserName, RoleCode = request.RoleCode });
            }
        }

        public async Task<UserRole> GetAsync(string userName, string roleCode)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,user_name AS UserName,role_code AS RoleCode,create_time AS CreateTime,update_time AS UpdateTime FROM role WHERE user_name=@UserName AND role_code=@RoleCode", new { UserName = userName, RoleCode = roleCode });
                if (data != null)
                {
                    return new UserRole()
                    {
                        Id = data.Id,
                        UserName = data.UserName,
                        RoleCode = data.RoleCode,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
            }
            return null;
        }
    }
}

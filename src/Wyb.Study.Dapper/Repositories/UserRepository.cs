using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos.Users;
using Wyb.Study.Domain.DbEntities;
using Wyb.Study.Domain.IRepositories;

namespace Wyb.Study.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateAsync(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("INSERT INTO User(user_name,password,create_time,update_time) VALUES(@UserName,@Password,@CreateTime,@UpdateTime);", new
                {
                    user.UserName,
                    user.Password,
                    user.CreateTime,
                    user.UpdateTime
                });
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("DELETE FROM User Where id=@Id", new
                {
                    Id = id
                });
            }
        }

        public async Task<int> UpdateAsync(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                return await conn.ExecuteAsync("UPDATE User set password=@Password,update_time=@UpdateTime WHERE id=@Id", new
                {
                    user.Password,
                    user.UpdateTime
                });
            }
        }

        public async Task<User> GetAsync(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                var data = await conn.QueryFirstOrDefaultAsync("SELECT id AS Id,user_name AS UserName,password AS Password,create_time AS CreateTime,update_time AS UpdateTime FROM User WHERE id=@Id", new { Id = id });
                if (data != null)
                {
                    return new User()
                    {
                        Id = data.Id,
                        UserName = data.UserName,
                        Password = data.Password,
                        CreateTime = data.CreateTime,
                        UpdateTime = data.UpdateTime
                    };
                }
                return null;
            }
        }

        public async Task<List<User>> GetListAsync(GetUserListDto getUserListDto)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT id AS Id,user_name AS UserName,password AS Password,create_time AS CreateTime,update_time AS UpdateTime FROM User";
                if (!string.IsNullOrWhiteSpace(getUserListDto.UserName))
                {
                    sql += " WHERE user_name like '%@UserName%'";
                }
                sql += $" LIMIT {(getUserListDto.PageIndex - 1) * getUserListDto.PageSize},{getUserListDto.PageSize}";
                var data = await conn.QueryAsync(sql, new { getUserListDto.UserName, getUserListDto.PageIndex, getUserListDto.PageSize });
                if (data != null)
                {
                    return data.Select(p => new User()
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        Password = p.Password,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                return new List<User>();
            }
        }

        public async Task<long> GetCountAsync(GetUserListDto getUserListDto)
        {
            using (MySqlConnection conn = new MySqlConnection(_configuration.GetSection("ConnectionStrings:Mysql").Value))
            {
                string sql = "SELECT COUNT(Id) FROM User";
                if (!string.IsNullOrWhiteSpace(getUserListDto.UserName))
                {
                    sql += " WHERE user_name like '%@UserName%'";
                }
                return await conn.ExecuteScalarAsync<int>(sql, new { getUserListDto.UserName });
            }
        }
    }
}

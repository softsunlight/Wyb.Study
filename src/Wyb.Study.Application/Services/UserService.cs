using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.Dtos.Users;
using Wyb.Study.IRepositories;
using Wyb.Study.IServices;

namespace Wyb.Study.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            return await _userRepository.CreateAsync(new User()
            {
                UserName = dto.UserName,
                Password = dto.Password,
            });
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<int> UpdateAsync(UpdateUserDto dto)
        {
            return await _userRepository.UpdateAsync(new User()
            {
                Id = dto.Id,
                UserName = dto.Password,
            });
        }

        public async Task<UserDto> GetAsync(long id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    CreateTime = user.CreateTime,
                    UpdateTime = user.UpdateTime
                };
            }
            return null;
        }

        public async Task<List<UserDto>> GetListAsync(GetUserListDto dto)
        {
            var userList = await _userRepository.GetListAsync(dto);
            if (userList != null)
            {
                return userList.Select(p => new UserDto()
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    Password = p.Password,
                    CreateTime = p.CreateTime,
                    UpdateTime = p.UpdateTime
                }).ToList();
            }
            return new List<UserDto>();
        }

        public async Task<long> GetCountAsync(GetUserListDto getUserListDto)
        {
            return await _userRepository.GetCountAsync(getUserListDto);
        }
    }
}

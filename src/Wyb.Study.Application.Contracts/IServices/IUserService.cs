using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos.Users;

namespace Wyb.Study.IServices
{
    public interface IUserService
    {
        Task<int> CreateAsync(CreateUserDto user);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(UpdateUserDto user);

        Task<UserDto> GetAsync(long id);

        Task<List<UserDto>> GetListAsync(GetUserListDto getUserListDto);

        Task<long> GetCountAsync(GetUserListDto getUserListDto);
    }
}

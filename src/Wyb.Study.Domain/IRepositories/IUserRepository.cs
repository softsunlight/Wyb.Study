using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.Dtos.Users;

namespace Wyb.Study.IRepositories
{
    public interface IUserRepository
    {

        Task<int> CreateAsync(User user);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(User user);

        Task<User> GetAsync(long id);

        Task<List<User>> GetListAsync(GetUserListDto getUserListDto);

        Task<long> GetCountAsync(GetUserListDto getUserListDto);
    }
}

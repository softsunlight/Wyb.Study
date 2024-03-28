using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos.Users;
using Wyb.Study.Domain.DbEntities;

namespace Wyb.Study.Domain.IRepositories
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

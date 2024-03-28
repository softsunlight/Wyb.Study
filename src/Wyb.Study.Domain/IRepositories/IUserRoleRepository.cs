using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Requests.UserRole;
using Wyb.Study.Domain.DbEntities;

namespace Wyb.Study.Domain.IRepositories
{
    public interface IUserRoleRepository
    {
        Task<int> CreateAsync(UserRole userRole);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(UserRole userRole);

        Task<UserRole> GetAsync(long id);

        Task<List<UserRole>> GetListAsync(GetUserRoleListRequest request);

        Task<long> GetCountAsync(GetUserRoleListRequest request);

        Task<UserRole> GetAsync(string userName, string roleCode);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.Requests.UserRole;

namespace Wyb.Study.IRepositories
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.Requests.Role;

namespace Wyb.Study.IRepositories
{
    public interface IRoleRepository
    {
        Task<int> CreateAsync(Role role);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(Role user);

        Task<Role> GetAsync(long id);

        Task<List<Role>> GetListAsync(GetRoleListRequest request);

        Task<long> GetCountAsync(GetRoleListRequest request);

        Task<Role> GetByRoleCodeAsync(string roleCode);
    }
}

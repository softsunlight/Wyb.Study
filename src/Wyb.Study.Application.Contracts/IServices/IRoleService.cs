using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos.Roles;
using Wyb.Study.Requests.Role;
using Wyb.Study.Responses;

namespace Wyb.Study.IServices
{
    public interface IRoleService
    {
        Task<BaseResponse> CreateAsync(CreateRoleRequest request);

        Task<BaseResponse> DeleteAsync(long id);

        Task<BaseResponse> UpdateAsync(UpdateRoleRequest request);

        Task<DataResponse<RoleDto>> GetAsync(long id);

        Task<PageListResponse<RoleDto>> GetListAsync(GetRoleListRequest request);
    }
}

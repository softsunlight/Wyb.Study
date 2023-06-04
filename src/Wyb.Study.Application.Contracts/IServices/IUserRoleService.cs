using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos.UserRoles;
using Wyb.Study.Requests.UserRole;
using Wyb.Study.Responses;

namespace Wyb.Study.IServices
{
    public interface IUserRoleService
    {
        Task<BaseResponse> CreateAsync(CreateUserRoleRequest request);

        Task<BaseResponse> DeleteAsync(long id);

        Task<BaseResponse> UpdateAsync(UpdateUserRoleRequest request);

        Task<DataResponse<UserRoleDto>> GetAsync(long id);

        Task<PageListResponse<UserRoleDto>> GetListAsync(GetUserRoleListRequest request);
    }
}

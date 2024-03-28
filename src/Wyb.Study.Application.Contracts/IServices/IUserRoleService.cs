using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos.UserRoles;
using Wyb.Study.Application.Contracts.Requests.UserRole;
using Wyb.Study.Application.Contracts.Responses;

namespace Wyb.Study.Application.Contracts.IServices
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

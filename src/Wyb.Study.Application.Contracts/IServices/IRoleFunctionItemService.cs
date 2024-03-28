using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos.RoleFunctionItems;
using Wyb.Study.Application.Contracts.Requests.RoleFunctionItem;
using Wyb.Study.Application.Contracts.Responses;

namespace Wyb.Study.Application.Contracts.IServices
{
    public interface IRoleFunctionItemService
    {
        Task<BaseResponse> CreateAsync(CreateRoleFunctionItemRequest request);

        Task<BaseResponse> DeleteAsync(long id);

        Task<BaseResponse> UpdateAsync(UpdateRoleFunctionItemRequest request);

        Task<DataResponse<RoleFunctionItemDto>> GetAsync(long id);

        Task<PageListResponse<RoleFunctionItemDto>> GetListAsync(GetRoleFunctionItemListRequest request);
    }
}

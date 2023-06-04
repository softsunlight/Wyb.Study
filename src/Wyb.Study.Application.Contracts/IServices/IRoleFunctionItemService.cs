using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos.RoleFunctionItems;
using Wyb.Study.Requests.RoleFunctionItem;
using Wyb.Study.Responses;

namespace Wyb.Study.IServices
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

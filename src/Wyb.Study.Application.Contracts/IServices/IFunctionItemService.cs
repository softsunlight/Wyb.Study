using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos.FunctionItems;
using Wyb.Study.Requests.FunctionItem;
using Wyb.Study.Responses;

namespace Wyb.Study.IServices
{
    public interface IFunctionItemService
    {
        Task<BaseResponse> CreateAsync(CreateFunctionItemRequest request);

        Task<BaseResponse> DeleteAsync(long id);

        Task<BaseResponse> UpdateAsync(UpdateFunctionItemRequest request);

        Task<DataResponse<FunctionItemDto>> GetAsync(long id);

        Task<PageListResponse<FunctionItemDto>> GetListAsync(GetFunctionItemListRequest request);
    }
}

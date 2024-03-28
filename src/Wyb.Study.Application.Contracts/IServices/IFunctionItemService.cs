using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos.FunctionItems;
using Wyb.Study.Application.Contracts.Requests.FunctionItem;
using Wyb.Study.Application.Contracts.Responses;

namespace Wyb.Study.Application.Contracts.IServices
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

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos;
using Wyb.Study.Application.Contracts.Dtos.FunctionItems;
using Wyb.Study.Application.Contracts.IServices;
using Wyb.Study.Application.Contracts.Requests.FunctionItem;
using Wyb.Study.Application.Contracts.Responses;
using Wyb.Study.Domain.DbEntities;
using Wyb.Study.Domain.IRepositories;

namespace Wyb.Study.Application.Services
{
    public class FunctionItemService : IFunctionItemService
    {
        private readonly ILogger<FunctionItemService> _logger;
        private readonly IFunctionItemRepository _functionItemRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FunctionItemService(ILogger<FunctionItemService> logger, IFunctionItemRepository functionItemRepository)
        {
            _logger = logger;
            _functionItemRepository = functionItemRepository;
        }

        public async Task<BaseResponse> CreateAsync(CreateFunctionItemRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _functionItemRepository.GetAsync(request.FunctionName, request.FunctionUrl);
                if (role != null)
                {
                    baseResponse.Message = "已存在相同的条目";
                    return baseResponse;
                }
                role = new FunctionItem()
                {
                    FunctionName = request.FunctionName,
                    FunctionUrl = request.FunctionUrl
                };
                var addResult = await _functionItemRepository.CreateAsync(role);
                if (addResult <= 0)
                {
                    baseResponse.Message = "添加失败";
                    return baseResponse;
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse> DeleteAsync(long id)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var deleteResult = await _functionItemRepository.DeleteAsync(id);
                if (deleteResult <= 0)
                {
                    baseResponse.Message = "删除失败";
                    return baseResponse;
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse> UpdateAsync(UpdateFunctionItemRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _functionItemRepository.GetAsync(request.Id);
                if (role == null)
                {
                    baseResponse.Message = "不存在该条目";
                    return baseResponse;
                }
                role = new FunctionItem()
                {
                    Id = request.Id,
                    FunctionName = request.FunctionName,
                };
                var deleteResult = await _functionItemRepository.UpdateAsync(role);
                if (deleteResult <= 0)
                {
                    baseResponse.Message = "更新失败";
                    return baseResponse;
                }
                baseResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        public async Task<DataResponse<FunctionItemDto>> GetAsync(long id)
        {
            DataResponse<FunctionItemDto> baseResponse = new DataResponse<FunctionItemDto>();
            try
            {
                var role = await _functionItemRepository.GetAsync(id);
                baseResponse.Success = true;
                if (role != null)
                {
                    baseResponse.Data = new FunctionItemDto()
                    {
                        Id = role.Id,
                        FunctionName = role.FunctionName,
                        FunctionUrl = role.FunctionUrl,
                        CreateTime = role.CreateTime,
                        UpdateTime = role.UpdateTime
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        public async Task<PageListResponse<FunctionItemDto>> GetListAsync(GetFunctionItemListRequest request)
        {
            PageListResponse<FunctionItemDto> baseResponse = new PageListResponse<FunctionItemDto>();
            try
            {
                var list = await _functionItemRepository.GetListAsync(request);
                var totalCount = await _functionItemRepository.GetCountAsync(request);
                baseResponse.Success = true;
                if (list != null)
                {
                    baseResponse.Items = list.Select(p => new FunctionItemDto()
                    {
                        Id = p.Id,
                        FunctionName = p.FunctionName,
                        FunctionUrl = p.FunctionUrl,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    }).ToList();
                }
                baseResponse.Page = new PageDto()
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询失败");
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.DbEntities;
using Wyb.Study.Dtos;
using Wyb.Study.Dtos.RoleFunctionItems;
using Wyb.Study.IRepositories;
using Wyb.Study.IServices;
using Wyb.Study.Requests.RoleFunctionItem;
using Wyb.Study.Responses;

namespace Wyb.Study.Services
{
    public class RoleFunctionItemService : IRoleFunctionItemService
    {
        private readonly ILogger<RoleFunctionItemService> _logger;
        private readonly IRoleFunctionItemRepository _roleFunctionItemRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RoleFunctionItemService(ILogger<RoleFunctionItemService> logger, IRoleFunctionItemRepository functionItemRepository)
        {
            _logger = logger;
            _roleFunctionItemRepository = functionItemRepository;
        }

        public async Task<BaseResponse> CreateAsync(CreateRoleFunctionItemRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _roleFunctionItemRepository.GetAsync(request.RoleCode, request.FunctionUrl);
                if (role != null)
                {
                    baseResponse.Message = "已存在相同角色编码的角色";
                    return baseResponse;
                }
                role = new RoleFunctionItem()
                {
                    RoleCode = request.RoleCode,
                    FunctionUrl = request.FunctionUrl
                };
                var addResult = await _roleFunctionItemRepository.CreateAsync(role);
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
                var deleteResult = await _roleFunctionItemRepository.DeleteAsync(id);
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

        public async Task<BaseResponse> UpdateAsync(UpdateRoleFunctionItemRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _roleFunctionItemRepository.GetAsync(request.Id);
                if (role == null)
                {
                    baseResponse.Message = "不存在该条目";
                    return baseResponse;
                }
                role = new RoleFunctionItem()
                {
                    Id = request.Id,
                    RoleCode = request.RoleCode,
                    FunctionUrl = request.FunctionUrl
                };
                var deleteResult = await _roleFunctionItemRepository.UpdateAsync(role);
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

        public async Task<DataResponse<RoleFunctionItemDto>> GetAsync(long id)
        {
            DataResponse<RoleFunctionItemDto> baseResponse = new DataResponse<RoleFunctionItemDto>();
            try
            {
                var role = await _roleFunctionItemRepository.GetAsync(id);
                baseResponse.Success = true;
                if (role != null)
                {
                    baseResponse.Data = new RoleFunctionItemDto()
                    {
                        Id = role.Id,
                        RoleCode = role.RoleCode,
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

        public async Task<PageListResponse<RoleFunctionItemDto>> GetListAsync(GetRoleFunctionItemListRequest request)
        {
            PageListResponse<RoleFunctionItemDto> baseResponse = new PageListResponse<RoleFunctionItemDto>();
            try
            {
                var list = await _roleFunctionItemRepository.GetListAsync(request);
                var totalCount = await _roleFunctionItemRepository.GetCountAsync(request);
                baseResponse.Success = true;
                if (list != null)
                {
                    baseResponse.Items = list.Select(p => new RoleFunctionItemDto()
                    {
                        Id = p.Id,
                        RoleCode = p.RoleCode,
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

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos;
using Wyb.Study.Application.Contracts.Dtos.Roles;
using Wyb.Study.Application.Contracts.IServices;
using Wyb.Study.Application.Contracts.Requests.Role;
using Wyb.Study.Application.Contracts.Responses;
using Wyb.Study.Domain.DbEntities;
using Wyb.Study.Domain.IRepositories;

namespace Wyb.Study.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RoleService(ILogger<RoleService> logger, IRoleRepository roleRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse> CreateAsync(CreateRoleRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                //先查询是否有相同的角色编码
                var role = await _roleRepository.GetByRoleCodeAsync(request.RoleCode);
                if (role != null)
                {
                    baseResponse.Message = "已存在相同角色编码的角色";
                    return baseResponse;
                }
                role = new Role()
                {
                    RoleName = request.RoleName,
                    RoleCode = request.RoleCode
                };
                var addResult = await _roleRepository.CreateAsync(role);
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
                var deleteResult = await _roleRepository.DeleteAsync(id);
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

        public async Task<BaseResponse> UpdateAsync(UpdateRoleRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _roleRepository.GetAsync(request.Id);
                if (role == null)
                {
                    baseResponse.Message = "不存在该角色";
                    return baseResponse;
                }
                role = new Role()
                {
                    Id = request.Id,
                    RoleName = request.RoleName,
                };
                var deleteResult = await _roleRepository.UpdateAsync(role);
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

        public async Task<DataResponse<RoleDto>> GetAsync(long id)
        {
            DataResponse<RoleDto> baseResponse = new DataResponse<RoleDto>();
            try
            {
                var role = await _roleRepository.GetAsync(id);
                baseResponse.Success = true;
                if (role != null)
                {
                    baseResponse.Data = new RoleDto()
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        RoleCode = role.RoleCode,
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

        public async Task<PageListResponse<RoleDto>> GetListAsync(GetRoleListRequest request)
        {
            PageListResponse<RoleDto> baseResponse = new PageListResponse<RoleDto>();
            try
            {
                var list = await _roleRepository.GetListAsync(request);
                var totalCount = await _roleRepository.GetCountAsync(request);
                baseResponse.Success = true;
                if (list != null)
                {
                    baseResponse.Items = list.Select(p => new RoleDto()
                    {
                        Id = p.Id,
                        RoleName = p.RoleName,
                        RoleCode = p.RoleCode,
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

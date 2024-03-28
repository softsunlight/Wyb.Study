using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos;
using Wyb.Study.Application.Contracts.Dtos.UserRoles;
using Wyb.Study.Application.Contracts.IServices;
using Wyb.Study.Application.Contracts.Requests.UserRole;
using Wyb.Study.Application.Contracts.Responses;
using Wyb.Study.Domain.DbEntities;
using Wyb.Study.Domain.IRepositories;

namespace Wyb.Study.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ILogger<UserRoleService> _logger;
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UserRoleService(ILogger<UserRoleService> logger, IUserRoleRepository userRoleRepository)
        {
            _logger = logger;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<BaseResponse> CreateAsync(CreateUserRoleRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                //先查询是否有相同的角色编码
                var userRole = await _userRoleRepository.GetAsync(request.UserName, request.RoleCode);
                if (userRole != null)
                {
                    baseResponse.Message = "该用户已存在该角色";
                    return baseResponse;
                }
                userRole = new UserRole()
                {
                    UserName = request.UserName,
                    RoleCode = request.RoleCode
                };
                var addResult = await _userRoleRepository.CreateAsync(userRole);
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
                var deleteResult = await _userRoleRepository.DeleteAsync(id);
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

        public async Task<BaseResponse> UpdateAsync(UpdateUserRoleRequest request)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var role = await _userRoleRepository.GetAsync(request.Id);
                if (role == null)
                {
                    baseResponse.Message = "不存在该用户角色";
                    return baseResponse;
                }
                role = new UserRole()
                {
                    Id = request.Id,
                    UserName = request.UserName,
                    RoleCode = request.RoleCode
                };
                var deleteResult = await _userRoleRepository.UpdateAsync(role);
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

        public async Task<DataResponse<UserRoleDto>> GetAsync(long id)
        {
            DataResponse<UserRoleDto> baseResponse = new DataResponse<UserRoleDto>();
            try
            {
                var role = await _userRoleRepository.GetAsync(id);
                baseResponse.Success = true;
                if (role != null)
                {
                    baseResponse.Data = new UserRoleDto()
                    {
                        Id = role.Id,
                        UserName = role.UserName,
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

        public async Task<PageListResponse<UserRoleDto>> GetListAsync(GetUserRoleListRequest request)
        {
            PageListResponse<UserRoleDto> baseResponse = new PageListResponse<UserRoleDto>();
            try
            {
                var list = await _userRoleRepository.GetListAsync(request);
                var totalCount = await _userRoleRepository.GetCountAsync(request);
                baseResponse.Success = true;
                if (list != null)
                {
                    baseResponse.Items = list.Select(p => new UserRoleDto()
                    {
                        Id = p.Id,
                        UserName = p.UserName,
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

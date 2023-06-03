using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.Dtos.Users;
using Wyb.Study.IServices;

namespace Wyb.Study.Http.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<dynamic> CreateAsync(CreateUserDto user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);
                if (result > 0)
                {
                    return new
                    {
                        Success = true,
                        Message = "添加成功"
                    };
                }
                else
                {
                    return new
                    {
                        Success = false,
                        Message = "添加失败"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new
                {
                    Success = false,
                    Message = "添加失败，" + ex.Message
                };
            }
        }

        [HttpDelete]
        public async Task<dynamic> DeleteAsync(long id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);
                if (result > 0)
                {
                    return new
                    {
                        Success = true,
                        Message = "删除成功"
                    };
                }
                else
                {
                    return new
                    {
                        Success = false,
                        Message = "删除失败"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new
                {
                    Success = false,
                    Message = "删除失败，" + ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<dynamic> UpdateAsync(UpdateUserDto user)
        {
            try
            {
                var result = await _userService.UpdateAsync(user);
                if (result > 0)
                {
                    return new
                    {
                        Success = true,
                        Message = "修改成功"
                    };
                }
                else
                {
                    return new
                    {
                        Success = false,
                        Message = "修改失败"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new
                {
                    Success = false,
                    Message = "修改失败，" + ex.Message
                };
            }
        }

        [HttpGet]
        public async Task<dynamic> GetAsync(long id)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                return new
                {
                    Success = true,
                    Message = "获取成功",
                    Item = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new
                {
                    Success = false,
                    Message = "修改失败，" + ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<dynamic> GetListAsync(GetUserListDto getUserListDto)
        {
            try
            {
                var users = await _userService.GetListAsync(getUserListDto);
                var count = await _userService.GetCountAsync(getUserListDto);
                return new
                {
                    Success = true,
                    Message = "获取成功",
                    Items = users,
                    TotalCount = count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new
                {
                    Success = false,
                    Message = "获取失败，" + ex.Message,
                    Items = new List<UserDto>(),
                    TotalCount = 0
                };
            }
        }
    }
}

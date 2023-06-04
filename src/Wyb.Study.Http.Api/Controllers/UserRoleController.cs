using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.IServices;
using Wyb.Study.Requests.UserRole;

namespace Wyb.Study.Http.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(ILogger<UserRoleController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<dynamic> CreateAsync(CreateUserRoleRequest request)
        {
            return await _userRoleService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<dynamic> DeleteAsync(long id)
        {
            return await _userRoleService.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> UpdateAsync(UpdateUserRoleRequest request)
        {
            return await _userRoleService.UpdateAsync(request);
        }

        [HttpGet]
        public async Task<dynamic> GetAsync(long id)
        {
            return await _userRoleService.GetAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> GetListAsync(GetUserRoleListRequest request)
        {
            return await _userRoleService.GetListAsync(request);
        }
    }
}

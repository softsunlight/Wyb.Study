using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.IServices;
using Wyb.Study.Requests.Role;

namespace Wyb.Study.Http.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<dynamic> CreateAsync(CreateRoleRequest request)
        {
            return await _roleService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<dynamic> DeleteAsync(long id)
        {
            return await _roleService.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> UpdateAsync(UpdateRoleRequest request)
        {
            return await _roleService.UpdateAsync(request);
        }

        [HttpGet]
        public async Task<dynamic> GetAsync(long id)
        {
            return await _roleService.GetAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> GetListAsync(GetRoleListRequest request)
        {
            return await _roleService.GetListAsync(request);
        }
    }
}

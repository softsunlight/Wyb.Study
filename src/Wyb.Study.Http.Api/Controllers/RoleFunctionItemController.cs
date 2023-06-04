using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.IServices;
using Wyb.Study.Requests.RoleFunctionItem;

namespace Wyb.Study.Http.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleFunctionItemController : ControllerBase
    {
        private readonly ILogger<RoleFunctionItemController> _logger;
        private readonly IRoleFunctionItemService _roleFunctionItemService;

        public RoleFunctionItemController(ILogger<RoleFunctionItemController> logger, IRoleFunctionItemService roleFunctionItemService)
        {
            _logger = logger;
            _roleFunctionItemService = roleFunctionItemService;
        }

        [HttpPost]
        public async Task<dynamic> CreateAsync(CreateRoleFunctionItemRequest request)
        {
            return await _roleFunctionItemService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<dynamic> DeleteAsync(long id)
        {
            return await _roleFunctionItemService.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> UpdateAsync(UpdateRoleFunctionItemRequest request)
        {
            return await _roleFunctionItemService.UpdateAsync(request);
        }

        [HttpGet]
        public async Task<dynamic> GetAsync(long id)
        {
            return await _roleFunctionItemService.GetAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> GetListAsync(GetRoleFunctionItemListRequest request)
        {
            return await _roleFunctionItemService.GetListAsync(request);
        }
    }
}

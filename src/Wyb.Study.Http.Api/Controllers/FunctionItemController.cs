using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.Application.Contracts.IServices;
using Wyb.Study.Application.Contracts.Requests.FunctionItem;

namespace Wyb.Study.Http.Api.Controllers
{
    /// <summary>
    ///程序功能管理控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class FunctionItemController : ControllerBase
    {
        private readonly ILogger<FunctionItemController> _logger;
        private readonly IFunctionItemService _functionItemService;

        public FunctionItemController(ILogger<FunctionItemController> logger, IFunctionItemService functionItemService)
        {
            _logger = logger;
            _functionItemService = functionItemService;
        }

        [HttpPost]
        public async Task<dynamic> CreateAsync(CreateFunctionItemRequest request)
        {
            return await _functionItemService.CreateAsync(request);
        }

        [HttpDelete]
        public async Task<dynamic> DeleteAsync(long id)
        {
            return await _functionItemService.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> UpdateAsync(UpdateFunctionItemRequest request)
        {
            return await _functionItemService.UpdateAsync(request);
        }

        [HttpGet]
        public async Task<dynamic> GetAsync(long id)
        {
            return await _functionItemService.GetAsync(id);
        }

        [HttpPost]
        public async Task<dynamic> GetListAsync(GetFunctionItemListRequest request)
        {
            return await _functionItemService.GetListAsync(request);
        }
    }
}

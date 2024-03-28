using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyb.Study.Application.Contracts.IServices;

namespace Wyb.Study.Http.Api.Controllers
{
    /// <summary>
    ///kafka示例管理控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class KafkaDemoController : ControllerBase
    {
        private readonly ILogger<KafkaDemoController> _logger;
        private readonly IKafkaDemoService _kafkaDemoService;

        public KafkaDemoController(ILogger<KafkaDemoController> logger, IKafkaDemoService kafkaDemoService)
        {
            _logger = logger;
            _kafkaDemoService = kafkaDemoService;
        }

        [HttpPost]
        public async Task<dynamic> SendMessageAsync()
        {
            return await _kafkaDemoService.SendMessageAsync();
        }

        [HttpPost]
        public async Task<dynamic> SendMessageTestDifferentKeyAsync()
        {
            return await _kafkaDemoService.SendMessageTestDifferentKeyAsync();
        }

        [HttpPost]
        public async Task<dynamic> SendMessageToAssignPartitionAsync()
        {
            return await _kafkaDemoService.SendMessageToAssignPartitionAsync();
        }

        [HttpPost]
        public async Task<dynamic> TransactionAsync()
        {
            return await _kafkaDemoService.TransactionAsync();
        }

    }
}

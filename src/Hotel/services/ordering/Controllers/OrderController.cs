using Microsoft.AspNetCore.Mvc;
using Nacos.V2;
using Newtonsoft.Json;
using ordering.entities;
using ordering.models;

namespace ordering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private INacosNamingService _nacosNamingService;

        private static readonly List<OrderVM> _orders = new List<OrderVM>() {
            new OrderVM {
                Id = "OD001",
                StartDay = "2021-05-01",
                EndDay = "2021-05-02",
                RoomNo = "1001",
                MemberId = "M001",
                HotelId = "H8001",
                CreateDay = "2021-05-01"
            }
        };

        public OrderController(ILogger<OrderController> logger, INacosNamingService nacosNamingService)
        {
            _logger = logger;
            _nacosNamingService = nacosNamingService;
        }

        [HttpGet("{id}")]
        public async Task<OrderVM> Get(string id)
        {
            var order = _orders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(order.MemberId))
            {
                var memberInstance = await _nacosNamingService.SelectOneHealthyInstance("member_center");
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"http://{memberInstance.Ip}:{memberInstance.Port}");
                    _logger.LogInformation($"ÊµÀý£º{memberInstance.Ip}:{memberInstance.Port}");
                    var memberResult = await httpClient.GetAsync("/member/" + order.MemberId);
                    var json = await memberResult.Content.ReadAsStringAsync();
                    var member = JsonConvert.DeserializeObject<MemberVM>(json);
                    order.Member = member;
                }
            }

            return order;
        }
    }
}
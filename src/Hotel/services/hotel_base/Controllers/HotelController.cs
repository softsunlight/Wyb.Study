using hotel_base.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private static List<HotelVM> _hotels = new List<HotelVM>() {
            new HotelVM{
                Id="H8001",
                Name = "��ǰ��",
                Phone = "50761111"
            },
            new HotelVM{
                Id="H8002",
                Name = "ʯ·��",
                Phone = "50761112"
            },

        };

        private readonly ILogger<HotelController> _logger;

        public HotelController(ILogger<HotelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<HotelVM> Get()
        {
            Task.Delay(10000).Wait();
            _logger.LogInformation($"hotel->get at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            return _hotels;
        }

        [HttpGet("{id}")]
        public HotelVM Get(string id)
        {
            return _hotels.FirstOrDefault(x => x.Id == id);
        }
    }
}
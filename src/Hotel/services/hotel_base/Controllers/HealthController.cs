using Microsoft.AspNetCore.Mvc;

namespace hotel_base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "ok";
        }
    }
}
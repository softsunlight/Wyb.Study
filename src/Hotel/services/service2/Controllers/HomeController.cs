using Microsoft.AspNetCore.Mvc;

namespace service2.Controllers
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
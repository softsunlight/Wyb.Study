using Microsoft.AspNetCore.Mvc;

namespace service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "ok";
        }
    }
}
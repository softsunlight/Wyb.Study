using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.Memory;
using Nacos.V2;
using Newtonsoft.Json.Linq;

namespace hotel_base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> Get(string section)
        {
            return _configuration.GetValue<string>(section) ?? "";
            //return await _nacosConfigService.GetConfig("hotolbaseconf", "DEFAULT_GROUP", 100);
        }

        [HttpPost]
        public async Task Add(string key, string value)
        {
            //serviceDescriptors.
            //var list = _configurationBuilder.Sources;
        }

        //[HttpGet("/GetConfig")]
        //public async Task<string> GetConfig(string key)
        //{
        //    return _configuration.GetValue<string>(key) ?? "";
        //}
    }
}
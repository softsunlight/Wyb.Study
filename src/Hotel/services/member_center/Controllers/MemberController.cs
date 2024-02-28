using member_center.models;
using Microsoft.AspNetCore.Mvc;

namespace member_center.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private static List<MemberVM> _members = new List<MemberVM>() {
            new MemberVM {Id = "M001", IdCard = "3333333333", Name ="����", Phone = "137777777", Sex ="M" },
            new MemberVM {Id = "M002", IdCard = "3333333334", Name ="����", Phone = "137777778", Sex ="M" },
            new MemberVM {Id = "M003", IdCard = "3333333335", Name ="С��", Phone = "137777779", Sex ="F" },
        };
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MemberVM> Get()
        {
            return _members;
        }

        [HttpGet("{id}")]
        public MemberVM Get(string id)
        {
            //throw new Exception("This api is mock fail .");

            return _members.FirstOrDefault(x => x.Id == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Application.Contracts.Dtos.Users
{
    public class GetUserListDto : PageDto
    {
        public string UserName { get; set; }
    }
}

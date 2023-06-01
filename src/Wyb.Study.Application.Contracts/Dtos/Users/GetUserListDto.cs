using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos;

namespace Wyb.Study.Dtos.Users
{
    public class GetUserListDto : PageDto
    {
        public string UserName { get; set; }
    }
}

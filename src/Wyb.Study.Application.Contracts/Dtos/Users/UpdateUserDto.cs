using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Application.Contracts.Dtos.Users
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string Password { get; set; }
    }
}

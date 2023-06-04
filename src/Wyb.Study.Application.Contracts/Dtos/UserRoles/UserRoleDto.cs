using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Dtos.UserRoles
{
    public class UserRoleDto : BaseDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色编码，唯一
        /// </summary>
        public string RoleCode { get; set; }
    }
}

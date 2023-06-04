using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Requests.RoleFunctionItem
{
    public class CreateRoleFunctionItemRequest
    {
        /// <summary>
        /// 角色编码，唯一
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 功能路由地址，唯一
        /// </summary>
        public string FunctionUrl { get; set; }
    }
}

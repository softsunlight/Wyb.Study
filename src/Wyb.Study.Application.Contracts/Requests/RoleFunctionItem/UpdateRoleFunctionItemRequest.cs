using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Application.Contracts.Requests.RoleFunctionItem
{
    public class UpdateRoleFunctionItemRequest
    {
        public long Id { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 功能路由地址
        /// </summary>
        public string FunctionUrl { get; set; }
    }
}

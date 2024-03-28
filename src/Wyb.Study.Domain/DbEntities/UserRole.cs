using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Domain.DbEntities
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// 用户名，唯一
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色编码，唯一
        /// </summary>
        public string RoleCode { get; set; }
    }
}

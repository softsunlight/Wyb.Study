﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Dtos;

namespace Wyb.Study.Application.Contracts.Dtos.Roles
{
    public class RoleDto : BaseDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色编码，唯一
        /// </summary>
        public string RoleCode { get; set; }
    }
}

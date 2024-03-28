using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Requests;

namespace Wyb.Study.Application.Contracts.Requests.FunctionItem
{
    public class GetFunctionItemListRequest : PageRequest
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 功能路由地址，唯一
        /// </summary>
        public string FunctionUrl { get; set; }
    }
}

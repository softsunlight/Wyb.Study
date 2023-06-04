using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Requests.FunctionItem
{
    public class CreateFunctionItemRequest
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

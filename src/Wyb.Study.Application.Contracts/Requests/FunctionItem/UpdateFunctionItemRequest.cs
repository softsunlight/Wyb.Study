using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Application.Contracts.Requests.FunctionItem
{
    public class UpdateFunctionItemRequest
    {
        public long Id { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Responses
{
    public class BaseResponse
    {
        /// <summary>
        /// 成功标识
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string Message { get; set; }
    }
}

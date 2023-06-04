using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Dtos;

namespace Wyb.Study.Responses
{
    public class PageListResponse<T> : BaseResponse
    {
        public List<T> Items { get; set; }
        public PageDto Page { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Dtos
{
    public class PageDto
    {
        public long PageIndex { get; set; } = 1;
        public long PageSize { get; set; } = 10;
        public long TotalCount { get; set; }
    }
}

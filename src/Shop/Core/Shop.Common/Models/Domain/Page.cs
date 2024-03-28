using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.Domain
{
    public class Page
    {
        public Page()
        {

        }

        public Page(int pageIndex, int pageSize, int total)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Total = total;
        }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int Total { get; set; }
    }
}

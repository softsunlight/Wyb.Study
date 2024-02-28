using Shop.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Domain.Response
{
    public class PageResponse<T> : BaseResponse
    {
        public PageResponse(bool success, int errorCode, string message, PageResult<T> data) : base(success, errorCode, message)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Data = data;
        }

        public static PageResponse<T> Successful(PageResult<T> data)
        {
            return new PageResponse<T>(true, (int)ErrorCodeEnum.Success, "success", data);
        }

        public PageResult<T> Data { get; set; }
    }

    public class PageResponse : PageResponse<object>
    {
        public PageResponse(bool success, int errorCode, string message, PageResult<object> data) : base(success, errorCode, message, data)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Data = data;
        }
    }

    public class PageResult<T>
    {

        public PageResult(List<T> items, Page page)
        {
            Items = items;
            Page = page;
        }

        public List<T> Items { get; set; }

        public Page Page { get; set; }
    }
}

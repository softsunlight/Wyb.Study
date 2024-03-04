using Hotel.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Domain.Response
{
    public class ObjectResponse<T> : BaseResponse
    {
        public ObjectResponse(bool success, int errorCode, string message, ObjectResult<T> data) : base(success, errorCode, message)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Data = data;
        }

        public static ObjectResponse<T> Successful(ObjectResult<T> data)
        {
            return new ObjectResponse<T>(true, (int)ErrorCodeEnum.Success, "success", data);
        }

        public ObjectResult<T> Data { get; set; }
    }

    public class ObjectResult<T>
    {
        public T Item { get; set; }
    }
}

using Hotel.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Domain.Response
{
    public class BaseResponse
    {
        public BaseResponse(bool success, int errorCode, string message)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        public static BaseResponse Successful()
        {
            return new BaseResponse(true, (int)ErrorCodeEnum.Success, "success");
        }

        public static BaseResponse Error(string message)
        {
            return new BaseResponse(false, (int)ErrorCodeEnum.Error, message);
        }

        public static BaseResponse Error(ErrorCodeEnum errorCode, string message)
        {
            return new BaseResponse(false, (int)errorCode, message);
        }

        public bool Success { get; set; }

        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }
}

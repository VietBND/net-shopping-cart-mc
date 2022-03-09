using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace VietBND.AspNetCore
{
    public class ShoppingCartControllerBase : ControllerBase
    {
        public override OkObjectResult Ok(object value)
        {
            var response = new BaseResponse()
            {
                Data = value,
                IsSuccess = true,
                ErrorMessages = new string[0]
            };
            return new OkObjectResult(response);
        }

        public SuccessObjectResult Success()
        {
            var response = new BaseResponse()
            {
                IsSuccess = true,
                ErrorMessages = new string[0]
            };
            return new SuccessObjectResult(response);
        }

        public override ObjectResult StatusCode(int statusCode, object value)
        {
            var response = new BaseResponse()
            {
                Data = value,
                IsSuccess = false,
                ErrorMessages = new string[0],
            };
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }

        public ObjectResult StatusCode(int statusCode, object value, string errorMessages)
        {
            var response = new BaseResponse()
            {
                Data = value,
                IsSuccess = false,
                ErrorMessages = new string[] { errorMessages },
            };
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
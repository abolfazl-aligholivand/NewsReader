using NewsReader.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace NewsReader.Domain.Helpers
{
    public class Response<T> : ControllerBase
    {
        public IActionResult ResponseSending(ApiResponse<T> response) => 
            (HttpStatusCodeEnum)response.Status switch
            {
                HttpStatusCodeEnum.Success => Ok(response),
                HttpStatusCodeEnum.BadRequest => BadRequest(response),
                HttpStatusCodeEnum.NotFound => NotFound(response),
                HttpStatusCodeEnum.Forbidden => Forbid(),
                _ => BadRequest(response)
            };
    }
}

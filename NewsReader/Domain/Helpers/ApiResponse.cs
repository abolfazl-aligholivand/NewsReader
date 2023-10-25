using NewsReader.Domain.Enums;

namespace NewsReader.Domain.Helpers
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }

        public ApiResponse(HttpStatusCodeEnum status, T result, string message)
        {
            Status = (int)status;
            Result = result;
            Message = message;
        }
    }
}

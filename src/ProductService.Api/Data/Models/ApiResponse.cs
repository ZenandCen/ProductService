namespace ProductService.Api.Data.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }


        // Constructor cho trường hợp thành công
        public static ApiResponse<T> Success(T data, int code = 200)
        {
            return new ApiResponse<T>
            {
                Status = "success",
                Code = code,
                Data = data,
                Message = null,
                Errors = null
            };
        }

        // Constructor cho trường hợp lỗi
        public static ApiResponse<T> Error(string message, int code, object errors = null)
        {
            return new ApiResponse<T>
            {
                Status = "error",
                Code = code,
                Data = default,
                Message = message,
                Errors = errors
            };
        }
    }

}

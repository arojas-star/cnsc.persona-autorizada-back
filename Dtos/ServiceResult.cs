namespace personaAutorizadaBack.Dtos
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Data = data,
                StatusCode = 200
            };
        }

        public static ServiceResult<T> Fail(string error, int statusCode)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Error = error,
                StatusCode = statusCode
            };
        }
    }
}

namespace WebDiary.BLL.Models.ServiceResponses
{
    public class ServiceDataResponse<T> : ServiceResponse
    {
        public T? Data { get; set; }

        public static new ServiceDataResponse<T> Fail(string message) 
        {
            return new ServiceDataResponse<T>
            {
                Message = message,
                Succeeded = false
            };
        }

        public static ServiceDataResponse<T> Success(T data)
        {
            return new ServiceDataResponse<T>
            {
                Data = data,
                Succeeded = true
            };
        }
    }
}
namespace WebDiary.BLL.Models
{
    public class ServiceDataResponse<T> : ServiceResponse
    {
        public T? Data { get; set; }
    }
}
namespace WebDiary.BLL.Models.ServiceResponses
{
    public class ServiceResponse
    {
        public string? Message { get; set; }
        public bool Succeeded { get; set; }

        public static ServiceResponse Fail(string message)
        {
            return new ServiceResponse
            {
                Message = message,
                Succeeded = false
            };
        }

        public static ServiceResponse Success(string message = null)
        {
            return new ServiceResponse
            {
                Message = message,
                Succeeded = true
            };
        }
    }
}
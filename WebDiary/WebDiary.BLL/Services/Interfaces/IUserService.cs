using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersAsync(string search);
    }
}

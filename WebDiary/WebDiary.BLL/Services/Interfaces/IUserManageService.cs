using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IUserManageService
    {
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersAsync();
        Task<ServiceDataResponse<UserDTO>> CreateUserAsync(string userName, string pass);
        Task<ServiceResponse> DeleteUserAsync(string userId);
    }
}
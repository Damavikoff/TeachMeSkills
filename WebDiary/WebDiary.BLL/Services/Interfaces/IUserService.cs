using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersContainsAsync(string search);
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersStartsWithAsync(string search);
        Task<ServiceDataResponse<List<UserDTO>>> GetFriendsAsync(string search, string authUserId);
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersAsync();
        Task<ServiceDataResponse<UserDTO>> GetUserByIdAsync(string userId);
        Task<ServiceDataResponse<UserDTO>> CreateUserAsync(string userName, string pass);
        Task<ServiceDataResponse<UserDTO>> ChangeUserNameAsync(UserDTO userModel);
        Task<ServiceDataResponse<UserDTO>> ChangeUserPasswordAsync(string userId, string newPass, HttpContext httpContext);
        Task<ServiceResponse> DeleteUserAsync(string userId);
    }
}
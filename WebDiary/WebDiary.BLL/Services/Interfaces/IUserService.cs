using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersContainsAsync(string search);
        Task<ServiceDataResponse<List<UserDTO>>> GetUsersStartsWithAsync(string search);
        Task<ServiceDataResponse<UserDTO>> GetUserByIdAsync(string userId);
        Task<ServiceDataResponse<UserDTO>> ChangeUserNameAsync(UserDTO userModel);
        Task<ServiceDataResponse<UserDTO>> ChangeUserPasswordAsync(string userId, string newPass, HttpContext httpContext);
    }
}
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IFriendsService
    {
        Task<ServiceDataResponse<List<FriendsDTO>>> GetFriendsAsync(string authUserId);
        Task<ServiceDataResponse<FriendsDTO>> GetFriendAsync(string friendId, string authUserId);
        Task<ServiceDataResponse<FriendsDTO>> AddFriendsAsync(FriendsDTO friendsModel, string authUserId);
        Task<ServiceResponse> DeleteFriendsAsync(string friendId, string authUserId);
    }
}
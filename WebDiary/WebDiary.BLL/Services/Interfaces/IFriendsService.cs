using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IFriendsService
    {
        Task<ServiceDataResponse<List<FriendsDTO>>> GetFriendsAsync(string authUserId);
        Task<ServiceDataResponse<List<FriendsDTO>>> GetFriendsBySearchAsync(string search, string authUserId);
        Task<ServiceDataResponse<FriendsDTO>> GetFriendAsync(string friendId, string authUserId);
        Task<ServiceDataResponse<FriendsDTO>> AddFriendAsync(FriendsDTO friendsModel, string authUserId);
        Task<ServiceResponse> DeleteFriendAsync(string friendId, string authUserId);
    }
}
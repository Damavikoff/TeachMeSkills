using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IGroupService
    {
        Task<ServiceDataResponse<List<GroupDTO>>> ShowUserGroups(string id);
        Task<ServiceDataResponse<GroupDTO>> GetGroupAsync(Guid groupId, string userId);
        Task<ServiceDataResponse<GroupDTO>> CreateGroupAsync(GroupDTO groupModel, string authUserId);
        Task<ServiceDataResponse<GroupDTO>> UpdateGroupAsync(GroupDTO groupModel, string authUserId); 
        Task<ServiceResponse> DeleteGroupAsync(Guid groupId, string authUserId);
    }
}

using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IGroupService
    {
        Task<ServiceDataResponse<List<GroupDTO>>> ShowUserGroups(string id);
        Task<ServiceResponse> CreateGroupAsync(GroupDTO groupModel, string authUserId);
        Task<ServiceResponse> DeleteGroupAsync(Guid groupId, string authUserId);
        Task<ServiceResponse> UpdateGroupAsync(GroupDTO groupModel, string authUserId); //или надо отдавать объект и на фронте его добавлять?
        Task<ServiceDataResponse<GroupDTO>> GetGroupAsync(Guid groupId, string userId);
    }
}

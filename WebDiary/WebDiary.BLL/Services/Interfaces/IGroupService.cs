using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IGroupService
    {
        Task<ServiceDataResponse<List<GroupDTO>>> ShowUserGroups(string id);
    }
}

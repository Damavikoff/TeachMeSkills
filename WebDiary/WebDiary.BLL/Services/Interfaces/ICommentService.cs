using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceDataResponse<List<EventDTO>>> LoadCommentsAsync(CommentDTO comment, string userId);
        Task<ServiceResponse> CreateCommentAsync(CommentDTO comment, string authUserId); //я ничего не возвращаю, т.к. при добавлении/апдейте/делите у меня повторно загружаются ивенты. 
        Task<ServiceResponse> UpdateCommentAsync(CommentDTO comment, string authUserId); //или надо отдавать объект и на фронте его добавлять?
        Task<ServiceResponse> DeleteCommentAsync(Guid commentId, string userId);
    }
}

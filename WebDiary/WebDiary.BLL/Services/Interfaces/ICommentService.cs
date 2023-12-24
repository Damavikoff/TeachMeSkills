using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceDataResponse<List<CommentDTO>>> LoadCommentsAsync(Guid eventId, string authUserId);
        Task<ServiceResponse> CreateCommentAsync(CommentDTO commentModel); //я ничего не возвращаю, т.к. при добавлении/апдейте/делите у меня повторно загружаются ивенты. 
        Task<ServiceResponse> UpdateCommentAsync(CommentDTO commentModel, string authUserId); //или надо отдавать объект и на фронте его добавлять?
        Task<ServiceResponse> DeleteCommentAsync(Guid commentId, string authUserId);
    }
}

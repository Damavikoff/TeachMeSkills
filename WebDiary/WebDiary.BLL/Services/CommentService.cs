using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;

namespace WebDiary.BLL.Services
{
    public class CommentService : ICommentService
    {
        public Task<ServiceResponse> CreateCommentAsync(CommentDTO comment, string authUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteCommentAsync(Guid commentId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDataResponse<List<EventDTO>>> LoadCommentsAsync(CommentDTO comment, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateCommentAsync(CommentDTO comment, string authUserId)
        {
            throw new NotImplementedException();
        }
    }
}

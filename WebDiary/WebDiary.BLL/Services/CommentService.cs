using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        public CommentService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create a new comment
        /// </summary>
        public async Task<ServiceResponse> CreateCommentAsync(CommentDTO commentModel)
        {
            if (commentModel.UserId == null)
                return ServiceResponse.Fail("You are not authenticated!");

            try
            {
                commentModel.Id = Guid.NewGuid();
                var obj = _mapper.Map<Comment>(commentModel);
                _webDiaryContext.Comments.Add(obj);
                await _webDiaryContext.SaveChangesAsync();

                return ServiceResponse.Success("Comment successfully created!");
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete existing comment 
        /// </summary>
        public async Task<ServiceResponse> DeleteCommentAsync(Guid commentId, string authUserId)
        {
            var obj = await _webDiaryContext.Comments.FirstOrDefaultAsync(p => p.Id == commentId);

            if (obj.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not delete this comment!");
            }

            if (obj == null)
            {
                return ServiceResponse.Fail("Comment is not founded!");
            }

            _webDiaryContext.Comments.Remove(obj);
            await _webDiaryContext.SaveChangesAsync();

            return ServiceResponse.Success("Comment successfully deleted!");
        }

        /// <summary>
        /// Loading all comments of an event
        /// </summary>
        /// <param name="authUserId">
        /// ID of an authenticated user
        /// </param>
        public async Task<ServiceDataResponse<List<CommentDTO>>> LoadCommentsAsync(Guid eventId, string authUserId)
        {
            //var objs = _webDiaryContext.Events.Include(p => p.Comments)
            //                                  .SingleOrDefault(p => p.Id == eventId);

            var objs = await _webDiaryContext.Comments.Where(e => e.EventId == eventId).OrderBy(d => d.CreatedDate).ToListAsync();

            if (objs == null)
                return ServiceDataResponse<List<CommentDTO>>.Fail("There are no comments!");

            var objsDTO = _mapper.Map<List<CommentDTO>>(objs);

            return ServiceDataResponse<List<CommentDTO>>.Success(objsDTO);
        }

        /// <summary>
        /// Update existing comment 
        /// </summary>
        public async Task<ServiceResponse> UpdateCommentAsync(CommentDTO commentModel, string authUserId)
        {
            if (commentModel.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not update this comment!");
            }

            try
            {
                var obj = _mapper.Map<Comment>(commentModel);
                _webDiaryContext.Comments.Update(obj);
                await _webDiaryContext.SaveChangesAsync();

                return ServiceResponse.Success("Comment successfully updated!");
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }
    }
}

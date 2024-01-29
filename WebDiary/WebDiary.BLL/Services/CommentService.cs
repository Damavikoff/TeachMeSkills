using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        /// Loading all comments of an event
        /// </summary>
        /// <param name="authUserId">
        /// ID of an authenticated user
        /// </param>
        public async Task<ServiceDataResponse<List<CommentDTO>>> GetCommentsAsync(Guid eventId, string authUserId)
        {
            var objs = await _webDiaryContext.Comments.Include(c => c.User)
                                                      .Include(c => c.ParentComment)
                                                      .Where(e => e.EventId == eventId)
                                                      .OrderBy(d => d.CreatedAt)
                                                      .ToListAsync();

            if (objs == null)
                return ServiceDataResponse<List<CommentDTO>>.Fail("There are no comments!");

            var objsDTO = _mapper.Map<List<CommentDTO>>(objs);

            return ServiceDataResponse<List<CommentDTO>>.Success(objsDTO);
        }

        /// <summary>
        /// Create a new comment
        /// </summary>
        public async Task<ServiceDataResponse<CommentDTO>> CreateCommentAsync(CommentDTO commentModel)
        {
            if (commentModel.UserId == null)
                return ServiceDataResponse<CommentDTO>.Fail("You are not authenticated!");
            
            if (string.IsNullOrWhiteSpace(commentModel.Content) || commentModel.Content.IsNullOrEmpty())
                return ServiceDataResponse<CommentDTO>.Fail("You can not create empty comment!");

            try
            {
                commentModel.Id = Guid.NewGuid();
                var obj = _mapper.Map<Comment>(commentModel);
                _webDiaryContext.Comments.Add(obj);
                await _webDiaryContext.SaveChangesAsync();

                return ServiceDataResponse<CommentDTO>.Success(commentModel);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<CommentDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Update existing comment 
        /// </summary>
        public async Task<ServiceDataResponse<CommentDTO>> UpdateCommentAsync(CommentDTO commentModel, string authUserId)
        {
            var obj = await _webDiaryContext.Comments.AsNoTracking()
                                                     .FirstOrDefaultAsync(p => p.Id == commentModel.Id);
            if (obj == null)
            {
                return ServiceDataResponse<CommentDTO>.Fail("Comment is not founded!");
            }

            if (obj.UserId != authUserId)
            {
                return ServiceDataResponse<CommentDTO>.Fail("You can not update this comment!");
            }

            try
            {
                obj.EditedAt = DateTime.Now;
                obj.Content = commentModel.Content;

                await _webDiaryContext.Comments.Where(u => u.Id == obj.Id)
                                        .ExecuteUpdateAsync(b => b
                                        .SetProperty(u => u.Content, obj.Content)
                                        .SetProperty(u => u.EditedAt, obj.EditedAt));

                var objDTO = _mapper.Map<CommentDTO>(obj);
                return ServiceDataResponse<CommentDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<CommentDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete existing comment 
        /// </summary>
        public async Task<ServiceResponse> DeleteCommentAsync(Guid commentId, string authUserId)
        {
            //или тут правильнее СервисДатаРеспонс и при успешном удалении вовзращать список комментов? 
            var obj = await _webDiaryContext.Comments.FirstOrDefaultAsync(p => p.Id == commentId);

            if (obj == null)
            {
                return ServiceResponse.Fail("Comment is not founded!");
            }

            if (obj.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not delete this comment!");
            }

            try
            {
                _webDiaryContext.Comments.Remove(obj);
                await _webDiaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }

            return ServiceResponse.Success("Comment successfully deleted!");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

namespace WebDiary.Controllers
{
    [Authorize(Roles = "user")]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        [HttpGet]
        public async Task<IActionResult> ShowEventCommentsPartial(Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.GetCommentsAsync(id, authUserId);
            var commentsViewModels = _mapper.Map<List<CommentViewModel>>(result.Data);
            return PartialView("ShowEventCommentsPartial", commentsViewModels);
        }

        [HttpGet]
        public IActionResult CreateCommentPartial() //CreateEventCommentPartial
        {
            return PartialView();
        }

        [HttpPost]
        [CommentValidationFilter]
        public async Task<IActionResult> CreateComment([FromBody] CommentViewModel comment)
        {
            //comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<CommentDTO>(comment);
            //проверить в блл комент на норм вид
            var result = await _commentService.CreateCommentAsync(objDTO);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            var commentsViewModels = _mapper.Map<CommentViewModel>(result.Data);

            if (commentsViewModels != null) // как в процессе создания коммента могут удалить коммент-то?
                return PartialView("ReturnedCommentPartial", commentsViewModels);

            return Ok();
        }

        [HttpPatch]
        [CommentValidationFilter]
        public async Task<IActionResult> UpdateComment([FromBody] CommentViewModel comment) //[FromBody] Guid Id
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<CommentDTO>(comment);

            var result = await _commentService.UpdateCommentAsync(objDTO, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost]
        //[CommentValidationFilter]
        public async Task<IActionResult> DeleteComment([FromBody] CommentViewModel comment) //[FromBody] Guid Id
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.DeleteCommentAsync(comment.Id, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            var loadComments = await _commentService.GetCommentsAsync(comment.EventId, authUserId);

            var commentsViewModels = _mapper.Map<List<CommentViewModel>>(loadComments.Data);

            return PartialView("ShowEventCommentsPartial", commentsViewModels);
        }
    }
}

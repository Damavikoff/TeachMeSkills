using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
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
        public async Task<IActionResult> CreateComment([FromBody] CommentViewModel comment)
        {
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<CommentDTO>(comment);
            //проверить в блл комент на норм вид
            var result = await _commentService.CreateCommentAsync(objDTO);

            var commentsViewModels = _mapper.Map<CommentViewModel>(result.Data);
            //if comment null...
            if (commentsViewModels != null)
                return PartialView("ReturnedCommentPartial", commentsViewModels);

            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromBody] CommentViewModel comment) //[FromBody] Guid Id
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.DeleteCommentAsync(comment.Id, authUserId);

            var loadComments = await _commentService.GetCommentsAsync(comment.EventId, authUserId);

            var commentsViewModels = _mapper.Map<List<CommentViewModel>>(loadComments.Data);

            return PartialView("ShowEventCommentsPartial", commentsViewModels); //ShowEventCommentsPartial
        }


        [HttpPost]
        public async Task<IActionResult> UpdateComment([FromBody] CommentViewModel comment) //[FromBody] Guid Id
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<CommentDTO>(comment);

            var result = await _commentService.UpdateCommentAsync(objDTO, authUserId);

            //var commentsViewModels = _mapper.Map<List<CommentViewModel>>(result.Data);

            //return PartialView("ShowEventCommentsPartial", commentsViewModels); //ShowEventCommentsPartial

            return Ok();
        }
    }
}

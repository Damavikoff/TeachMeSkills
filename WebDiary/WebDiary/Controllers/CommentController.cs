using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

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
        //[HttpGet]
        public async Task<IActionResult> ShowEventCommentsPartial(Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.LoadCommentsAsync(id, authUserId);
            var objsViewModels = _mapper.Map<List<CommentViewModel>>(result.Data);
            return PartialView("ShowEventCommentsPartial", objsViewModels);
        }

        [HttpGet]
        public IActionResult CreateCommentPartial() //CreateEventCommentPartial
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult CreateComment([FromBody] CommentViewModel comment)
        {
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<CommentDTO>(comment);
            var result =  _commentService.CreateCommentAsync(objDTO);
            return PartialView("ShowEventCommentsPartial");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComment(Guid Id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _commentService.DeleteCommentAsync(Id, authUserId);
            return Ok(result.Message);
        }
    }
}

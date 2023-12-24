using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Views.Event.Components.CommentComponent
{
    public class CommentComponent : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentComponent(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string eventId, string authUserId)
        {
            //var result = await _commentService.LoadCommentsAsync(eventId, authUserId);
            //var objsViewModels = _mapper.Map<List<CommentViewModel>>(result.Data);
            var objsViewModels = new List<CommentViewModel>();
            return View(objsViewModels);
        }
        
    }
}

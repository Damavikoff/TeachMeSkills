using Blog.Models;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IReadService _readService;

        public PostController(IPostService postService, IReadService readService)
        {
            _postService = postService;
            _readService = readService; 
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_readService.ShowArticles());
        }

        public IActionResult ShowArticle([FromRoute] Guid id)
        {
            var articleDetails = _readService.ShowArticleDetails(id);
            return View(articleDetails);
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [PostValidationFilter]
        [HttpPost]
        public IActionResult CreateArticle(ArticleDTO post)
        {
            _postService.CreateArticle(post);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult WriteComment()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult WriteComment(CommentDTO comment)
        {
            _postService.CreateComment(comment);
            return RedirectToAction("Index");
        }

        public class PostValidationFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var postObject = context.ActionArguments.SingleOrDefault(p => p.Value is ArticleDTO);
                if (postObject.Value == null)
                {
                    context.Result = new BadRequestObjectResult("Post value cannot be null");
                    return;
                }

                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
        }
    }
}
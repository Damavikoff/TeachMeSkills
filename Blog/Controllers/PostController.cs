using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_postService.GetPosts());
        }

        [HttpGet]
        public IActionResult Details([FromRoute] Guid id)
        {
            var post = _postService.GetPost(id);
            if(post == null)
                return NotFound();
            return View(post);
        }

        //public IActionResult Get()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var comments = _postService.GetComments(id);
            return View(comments);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_postService.GetPosts());
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        [PostValidationFilter]
        [HttpPost]
        public IActionResult Post(Article post)
        {
            _postService.AddPost(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Comment(Comment comment)
        {
            _postService.AddComment(comment);
            return RedirectToAction("Index");
        }

        public class PostValidationFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var postObject = context.ActionArguments.SingleOrDefault(p => p.Value is Article);
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
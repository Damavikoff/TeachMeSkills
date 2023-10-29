using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController() 
        {
            _postService = new PostService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_postService.GetPosts());
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            var post = _postService.GetPost(id);
            if(post == null)
                return NotFound();
            return View(post);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Get(int id)
        {
            return View(_postService.GetPost(id));
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
        public IActionResult Post(Post post)
        {
            _postService.AddPost(post);
            return RedirectToAction("Index");
        }

        public class PostValidationFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var postObject = context.ActionArguments.SingleOrDefault(p => p.Value is Post);
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
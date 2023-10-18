using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController() 
        {
            _postService = new PostService();
        }
        public ActionResult Index()
        {
            return View(_postService.GetPosts());
        }
        public ActionResult Get()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Get(int id)
        {
            return View(_postService.GetPost(id));
        }

        //[HttpGet]
        public ActionResult GetAll()
        {
            return Json(_postService.GetPosts());
        }
        public IActionResult Post()
        {
            return View();
        }
        [HttpPost]//если не указать, то метод может принимать все типы запросов. Если указать GET, то метод не будет работать как предполагалось 
        public async Task<IActionResult> Post(Post post)
        {
            _postService.AddPost(post);
            return RedirectToAction("Index");
        }
    }
}
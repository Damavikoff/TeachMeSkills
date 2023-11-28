using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Diagnostics;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(new Event());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            return Json(GenerateEvents());
        }

        public List<Event> GenerateEvents()
        {
            using (WebDiaryContext db = new WebDiaryContext())
            {
                var Posts = db.Events.ToList();
                return Posts;
            }
        }
        
        [HttpPost]
        public IActionResult AddEvent(Event eventModel)
        {
            using (WebDiaryContext db = new WebDiaryContext())
            {
                db.Events.Add(eventModel);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
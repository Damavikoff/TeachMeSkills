using Lesson26.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Lesson26.Services;

namespace Lesson26.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISparjaService _sparjaService;

        public HomeController(ISparjaService sparjaService)
        {
            _sparjaService = sparjaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_sparjaService.GetSparjas());
        }

        [HttpGet]
        public IActionResult AddSparja()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSparja(Sparja sparjaModel)
        {
            _sparjaService.AddSparja(sparjaModel);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
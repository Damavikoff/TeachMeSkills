using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Diagnostics;
using WebDiary.Models;
using WebDiary.Services.Interfaces;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }
        public IActionResult Index()
        {
            return View(new EventDTO());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult LoadEvents()
        {
            return Json(_eventService.LoadEvents());
        }

        //[HttpGet]
        public JsonResult GetEvent(Guid id)
        {
            return Json(_eventService.GetEvent(id));
        }

        [HttpPost]
        public JsonResult CreateEvent([FromBody] EventDTO eventModel)
        {
            if (ModelState.IsValid)
            { 
                _eventService.CreateEvent(eventModel);
                return Json("Event successfully added!");
            }
            return Json("Something goes wrong...");
        }

        [HttpPost]
        public JsonResult UpdateEvent([FromBody] EventDTO eventModel)
        {
            if (ModelState.IsValid)
            {
                _eventService.UpdateEvent(eventModel);
                return Json("Event successfully updated!");
            }
            return Json("Something goes wrong...");
        }

        [HttpPost]
        public JsonResult DeleteEvent([FromBody] Guid id)
        {
            _eventService.DeleteEvent(id);
            return Json("ok");
        }
    }
}
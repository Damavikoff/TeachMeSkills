using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;
using WebDiary.Services.Interfaces;

namespace WebDiary.Controllers
{
    //rename to EventController
    //rename layers
    //add logger
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

        //do i need an action filter for GET?
        //if model null ...
        [HttpGet] //was commented
        public JsonResult GetEvent(Guid id)
        {
            return Json(_eventService.GetEvent(id));
        }

        [EventValidationFilter]
        [HttpPost]
        public JsonResult CreateEvent([FromBody] EventDTO eventModel)
        {
            _eventService.CreateEvent(eventModel);
            return Json("Event successfully added!");
        }

        [EventValidationFilter]
        [HttpPost]
        public JsonResult UpdateEvent([FromBody] EventDTO eventModel)
        {
            _eventService.UpdateEvent(eventModel);
            return Json("Event successfully updated!");
        }

        [GuidValidationFilter]
        [HttpPost]
        public JsonResult DeleteEvent([FromBody] Guid id)
        {
            _eventService.DeleteEvent(id);
            return Json("Event successfully deleted!");
            //if (ModelState.IsValid)
            //{
            //    _eventService.DeleteEvent(id);
            //    return Json("Event successfully deleted!");
            //}
            //return Json("Something goes wrong...");
        }
    }
}
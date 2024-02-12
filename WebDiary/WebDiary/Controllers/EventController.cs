using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

namespace WebDiary.Controllers
{
    [Authorize(Roles = "user")]
    public class EventController : Controller //TODO: add logger
    {
        //private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IActionResult Index()
        {
            return View(new EventViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> LoadEvents(DateTime start, DateTime end) //LoadEventsByTime + add to interface usual LoadEvents (all)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.LoadEventsAsync(start, end, userId);

            //if (result.Succeeded == false) rawevents is not iterable
            //{
            //    return Json(result);
            //}

            var objsViewModels = _mapper.Map<List<EventViewModel>>(result.Data);
            return Json(objsViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.GetEventAsync(id, authUserId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message); 
            }

            var objViewModel = _mapper.Map<EventViewModel>(result.Data);
            return Ok(objViewModel);
        }

        [HttpPost]
        [EventValidationFilter]
        public async Task<IActionResult> CreateEvent([FromBody] EventViewModel eventModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.CreateEventAsync(objDTO, authUserId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            //var objViewModel = _mapper.Map<EventViewModel>(result.Data);
            return Json(result.Message); //return Ok is not refetched events
        }

        [HttpPost]
        [EventValidationFilter]
        public async Task<IActionResult> UpdateEvent([FromBody] EventViewModel eventModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.UpdateEventAsync(objDTO, authUserId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            //var objViewModel = _mapper.Map<EventViewModel>(result.Data);
            return Json(result.Message);
        }

        [HttpPost]
        [GuidValidationFilter]
        public async Task<IActionResult> DeleteEvent([FromBody] Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.DeleteEventAsync(id, authUserId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Json(result.Message);
        }
    }
}
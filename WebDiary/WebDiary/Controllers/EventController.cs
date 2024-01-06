using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

namespace WebDiary.Controllers
{
    //add logger
    public class EventController : Controller
    {
        //private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly IGroupService _groupService;

        public EventController(IEventService eventService, IGroupService groupService, IMapper mapper, ICommentService commentService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
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
        public async Task<IActionResult> LoadEvents(DateTime start, DateTime end)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.LoadEventsAsync(start, end, userId);

            //if (result.Succeeded == false)
            //{
            //    return Json(result); //
            //}

            var objsViewModels = _mapper.Map<List<EventViewModel>>(result.Data);

            return Json(objsViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.GetEventAsync(id, authUserId);

            if (result.Succeeded == false)
            {
                return Json(result.Message); //как-то поправить, мне не нравится что возвращает джсон а не что-то типа BadRequest
            }

            var objViewModels = _mapper.Map<EventViewModel>(result.Data);
            return Ok(objViewModels);
        }

        [HttpPost]
        [EventValidationFilter]
        public async Task<IActionResult> CreateEvent([FromBody] EventViewModel eventModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.CreateEventAsync(objDTO, authUserId);
            return Json(result.Message); //возвращать объект?
        }

        [HttpPost]
        [EventValidationFilter]
        public async Task<IActionResult> UpdateEvent([FromBody] EventViewModel eventModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.UpdateEventAsync(objDTO, authUserId);
            return Json(result.Message);
        }

        [HttpPost]
        [GuidValidationFilter]
        public async Task<IActionResult> DeleteEvent([FromBody] Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _eventService.DeleteEventAsync(id, authUserId);
            return Json(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> ShowUserGroupsDropDownPartial()
        {
            var result = await _groupService.ShowUserGroups(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var objsViewModels = _mapper.Map<List<GroupViewModel>>(result.Data);
            return PartialView(objsViewModels);
        }
    }
}
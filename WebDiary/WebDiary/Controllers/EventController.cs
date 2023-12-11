using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

namespace WebDiary.Controllers
{
    //add logger
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        //IMapper mmaper;
        private readonly IEventService _eventService;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;

            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.AddMaps(new[] {
            //       typeof(WebDiary.BLL.Models.EventDTO),
            //        typeof(EventViewModel)
            //      }));

            //mmaper = configuration.CreateMapper();
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
        public async Task<IActionResult> LoadEvents()//(DateTime startTime, DateTime endTime)
        {
            var result = await _eventService.LoadEventsAsync();

            if (result.Succeeded == false)
            {
                return Json(result.Message); //как-то поправить + при загрузке ивентов только на месяц каждую прокрутку будет ругаться?
            }

            var objsViewModels = _mapper.Map<List<EventViewModel>>(result.Data);
            return Json(objsViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var result = await _eventService.GetEventAsync(id);

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
            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.CreateEventAsync(objDTO);
            return Json(result.Message); //возвращать объект?
        }

        [HttpPost]
        [EventValidationFilter]
        public async Task<IActionResult> UpdateEvent([FromBody] EventViewModel eventModel)
        {
            var objDTO = _mapper.Map<EventDTO>(eventModel);
            var result = await _eventService.UpdateEventAsync(objDTO);
            return Json(result.Message);
        }

        [HttpPost]
        [GuidValidationFilter]
        public async Task<IActionResult> DeleteEvent([FromBody] Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            return Json(result.Message);
        }
    }
}
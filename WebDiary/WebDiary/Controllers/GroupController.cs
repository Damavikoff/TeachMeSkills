using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class GroupController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult NewGroupPartial() //CreateEventCommentPartial
        {
            return PartialView("NewGroupPartial");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupViewModel groupModel)
        {
            //попробовать тут создать груп модель со всеми данными и посмотреть как она вынесется в БД.
            //Нужен ли объект юзера или можно ид юзера передать чтоб в таблице юзергрупс все норм замапилось?
            //может на клиенте делать запрос к БД на юзеров? или как-то 
            //
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<GroupDTO>(groupModel);
            var result = await _groupService.CreateGroupAsync(objDTO, authUserId);
            return Json(result.Message);
        }

        [HttpGet]
        public IActionResult ExistGroupPartial() //CreateEventCommentPartial
        {
            return PartialView("ExistGroupPartial");
        }

    }
}

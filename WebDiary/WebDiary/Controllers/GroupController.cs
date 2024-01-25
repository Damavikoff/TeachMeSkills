using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

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
        public IActionResult ManageGroupPartial() //CreateEventCommentPartial
        {
            return PartialView("ManageGroupPartial");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupViewModel groupModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<GroupDTO>(groupModel);
            var result = await _groupService.CreateGroupAsync(objDTO, authUserId);
            return Json(result.Message);
        }

        //[HttpGet]
        //public IActionResult ExistGroupPartial() //CreateEventCommentPartial
        //{
        //    return PartialView("ExistGroupPartial");
        //}

        [HttpGet]
        public async Task<IActionResult> GetGroup(Guid groupId) //CreateEventCommentPartial
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _groupService.GetGroupAsync(groupId, authUserId);

            if (result.Succeeded == false)
            {
                return Json(result.Message); //как-то поправить, мне не нравится что возвращает джсон а не что-то типа BadRequest
            }

            var objViewModel = _mapper.Map<GroupViewModel>(result.Data);
            return Ok(objViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup([FromBody] Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _groupService.DeleteGroupAsync(id, authUserId);
            return Json(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupViewModel groupModel)
        {
            //here i get userId. And i have userId in groupModel (for validate in Groupservice?)
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<GroupDTO>(groupModel);
            var result = await _groupService.UpdateGroupAsync(objDTO, authUserId);
            return Json(result.Message);
        }
    }
}

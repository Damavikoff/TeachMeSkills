using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;
using WebDiary.Services.FilterAttributes;

namespace WebDiary.Controllers
{
    [Authorize(Roles = "user")]
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
        public async Task<IActionResult> Index()
        {
            var result = await _groupService.ShowUserGroups(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (result.Succeeded == false)
            {
                return Json(result.Message);
            }

            var obj = _mapper.Map<List<GroupViewModel>>(result.Data);
            return View(obj);
        }

        [HttpGet]
        public IActionResult ManageGroupPartial()
        {
            return PartialView("ManageGroupPartial");
        }

        [HttpGet]
        public async Task<IActionResult> ShowUserGroupsDropDownPartial() 
        {
            var result = await _groupService.ShowUserGroups(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var objsViewModels = _mapper.Map<List<GroupViewModel>>(result.Data);
            return PartialView(objsViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup(Guid groupId)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _groupService.GetGroupAsync(groupId, authUserId);

            if (result.Succeeded == false)
            {
                return Json(result.Message); 
            }

            var objViewModel = _mapper.Map<GroupViewModel>(result.Data);
            return Ok(objViewModel);
        }

        [HttpPost]
        [GroupValidationFilter]
        public async Task<IActionResult> CreateGroup([FromBody] GroupViewModel groupModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<GroupDTO>(groupModel);
            var result = await _groupService.CreateGroupAsync(objDTO, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            var objViewModel = _mapper.Map<GroupViewModel>(result.Data);
            return PartialView("ReturnedGroupPartial", objViewModel);
        }

        [HttpPost]
        [GroupValidationFilter]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupViewModel groupModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<GroupDTO>(groupModel);
            var result = await _groupService.UpdateGroupAsync(objDTO, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            return Json(result.Message);
        }

        [HttpPost]
        [GuidValidationFilter]
        public async Task<IActionResult> DeleteGroup([FromBody] Guid id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _groupService.DeleteGroupAsync(id, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            return Json(result.Message);
        }
    }
}
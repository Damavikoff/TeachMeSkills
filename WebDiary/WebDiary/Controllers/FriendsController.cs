using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    //friend list
    [Authorize(Roles = "user")]
    public class FriendsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFriendsService _friendsService;
        public FriendsController(IMapper mapper, IFriendsService friendsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _friendsService = friendsService ?? throw new ArgumentNullException(nameof(friendsService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _friendsService.GetFriendsAsync(authUserId);

            if (result.Succeeded == false)
            {
                return Json(result.Message);
            }

            var obj = _mapper.Map<List<FriendsViewModel>>(result.Data);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend([FromBody] FriendsViewModel friendModel)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var objDTO = _mapper.Map<FriendsDTO>(friendModel);
            var result = await _friendsService.AddFriendsAsync(objDTO, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            var result2 = await _friendsService.GetFriendAsync(friendModel.FriendId, authUserId);

            if (result2.Succeeded == false)
            {
                return BadRequest(result2.Message);
            }

            var objViewModel = _mapper.Map<FriendsViewModel>(result2.Data);
            return Json(objViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _friendsService.DeleteFriendsAsync(id, authUserId);

            if (result.Succeeded == false)
            {
                return BadRequest(result.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
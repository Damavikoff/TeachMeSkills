using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    //user manage
    [Authorize(Roles = "user")]
    public class UserController : Controller //TODO: add friend list
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFriendsService _friendsService;

        public UserController(IUserService userService, IMapper mapper, IFriendsService friendsService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _friendsService = friendsService ?? throw new ArgumentNullException(nameof(friendsService));
        }

        [HttpPost]
        public async Task<IActionResult> SearchUserByAuthUser(string search) 
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _friendsService.GetFriendsBySearchAsync(search, authUserId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            var objsViewModels = new List<UserViewModel>();

            foreach (var user in result.Data)
            {
                objsViewModels.Add(_mapper.Map<UserViewModel>(user.Friend));
            }
            
            return Ok(objsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string search)
        {
            var result = await _userService.GetUsersStartsWithAsync(search);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            var objsViewModels = _mapper.Map<List<UserViewModel>>(result.Data);
            return Ok(objsViewModels);
        }

        public async Task<IActionResult> Index()
        {
            return RedirectPermanent("/User/Profile");
        }

        public async Task<IActionResult> Profile()
        {
            var result = await _userService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return View(result.Data);
        }

        public async Task<IActionResult> AccountDetails(string id)
        {
            return PartialView("AccountDetailsPartial");
        }

        public async Task<IActionResult> Security()
        {
            var result = await _userService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var objDto = _mapper.Map<UserDTO>(model);
                var result = await _userService.ChangeUserNameAsync(objDto);

                if (!result.Succeeded)
                {
                    return Json(result.Message);
                }
                
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            return PartialView("ChangePassword");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditUserViewModel userModel)
        {
            var test = HttpContext; //DI container?
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangeUserPasswordAsync(userModel.Id, userModel.NewPassword, test);

                if (!result.Succeeded)
                {
                    return Json(result.Message);
                }

                return RedirectToAction("Index");
            }
            return View(userModel);
        }
    }
}
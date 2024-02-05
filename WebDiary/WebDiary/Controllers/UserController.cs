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
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> SearchUserByAuthUser(string search) 
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.GetFriendsAsync(search, authUserId);
            var objsViewModels = _mapper.Map<List<UserViewModel>>(result.Data);
            return Ok(objsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string search)
        {
            var result = await _userService.GetUsersStartsWithAsync(search);
            var objsViewModels = _mapper.Map<List<UserViewModel>>(result.Data);
            return Ok(objsViewModels);
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(result.Data);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return PartialView("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var objDto = _mapper.Map<UserDTO>(model);
                var result = await _userService.ChangeUserNameAsync(objDto);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Json(result.Message);
                }
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
            var test = HttpContext;
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangeUserPasswordAsync(userModel.Id, userModel.NewPassword, test);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Json(result.Message);
                }
            }
            return View(userModel);
        }
    }
}
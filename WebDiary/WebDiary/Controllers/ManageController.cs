using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    //admin panel 
    [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserManageService _userManageService;

        public ManageController(IUserService userService, IMapper mapper, IUserManageService userManageService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userManageService = userManageService ?? throw new ArgumentNullException(nameof(userManageService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objs = await _userManageService.GetUsersAsync();
            var objsViewModel = _mapper.Map<List<UserViewModel>>(objs.Data);
            return View(objsViewModel);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManageService.CreateUserAsync(model.Email, model.Password);

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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (!result.Succeeded)
            {
                return Json(result.Message);
                
            }

            EditUserViewModel model = new EditUserViewModel { Id = result.Data.Id, Email = result.Data.Email };
            return View(model);
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

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _userManageService.DeleteUserAsync(id);

            if (!result.Succeeded)
            {
                return Json(result.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (!result.Succeeded)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel { Id = result.Data.Id, Email = result.Data.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditUserViewModel userModel)
        {
            var test = HttpContext;
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
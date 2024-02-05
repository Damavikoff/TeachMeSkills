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

        public ManageController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objs = await _userService.GetUsersAsync();
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
                //var obj = _mapper.Map<UserDTO>(model);
                var result = await _userService.CreateUserAsync(model.Email, model.Password);

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
            if (result.Succeeded)
            {
                EditUserViewModel model = new EditUserViewModel { Id = result.Data.Id, Email = result.Data.Email };
                return View(model);
            }
            else
            {
                return Json(result.Message);
            }
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

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Json(result.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.Succeeded)
            {
                EditUserViewModel model = new EditUserViewModel { Id = result.Data.Id, Email = result.Data.Email };
                return View(model);
            }
            else
            {
                return NotFound();
            }
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
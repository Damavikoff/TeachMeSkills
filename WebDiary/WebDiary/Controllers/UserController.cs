using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpPost]
        public async Task<IActionResult> SearchUser(string search) 
        {
            var result = await _userService.GetUsersAsync(search);
            var objsViewModels = _mapper.Map<List<UserViewModel>>(result.Data);
            return Ok(objsViewModels);
        }
    }
}

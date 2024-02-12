using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        private readonly UserManager<User> _userManager;
        public UserService(WebDiaryContext webDiaryContext, IMapper mapper, UserManager<User> userManager)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get user by UserName which contains search string
        /// </summary>
        public async Task<ServiceDataResponse<List<UserDTO>>> GetUsersContainsAsync(string search)
        {
            var obj = await _webDiaryContext.Users.Where(p => p.UserName.Contains(search)).ToListAsync();

            if (obj == null)
                return ServiceDataResponse<List<UserDTO>>.Fail("User is not exist!");

            var objDTO = _mapper.Map<List<UserDTO>>(obj);

            return ServiceDataResponse<List<UserDTO>>.Success(objDTO);
        }

        /// <summary>
        /// Get user by UserName which starts with search string
        /// </summary>
        public async Task<ServiceDataResponse<List<UserDTO>>> GetUsersStartsWithAsync(string search)
        {
            var obj = await _webDiaryContext.Users.Where(p => p.UserName.StartsWith(search)).ToListAsync();

            if (obj == null)
                return ServiceDataResponse<List<UserDTO>>.Fail("User is not exist!");

            var objDTO = _mapper.Map<List<UserDTO>>(obj);

            return ServiceDataResponse<List<UserDTO>>.Success(objDTO);
        }

        /// <summary>
        /// Get user by user id
        /// </summary>
        public async Task<ServiceDataResponse<UserDTO>> GetUserByIdAsync(string userId)
        {
            var obj = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (obj == null)
                return ServiceDataResponse<UserDTO>.Fail("User is not exist!");

            var objDTO = _mapper.Map<UserDTO>(obj);

            return ServiceDataResponse<UserDTO>.Success(objDTO);
        }

        /// <summary>
        /// Change user username
        /// </summary>
        public async Task<ServiceDataResponse<UserDTO>> ChangeUserNameAsync(UserDTO userModel)
        {
            var obj = await _userManager.FindByIdAsync(userModel.Id);

            if (obj == null)
            {
                return ServiceDataResponse<UserDTO>.Fail("User not founded!");
            }

            obj.Email = userModel.Email;
            obj.UserName = userModel.UserName;

            try
            {
                await _userManager.UpdateAsync(obj);
                var objDTO = _mapper.Map<UserDTO>(obj);

                return ServiceDataResponse<UserDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<UserDTO>.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Change user password
        /// </summary>
        public async Task<ServiceDataResponse<UserDTO>> ChangeUserPasswordAsync(string userId, string newPass, HttpContext httpContext)
        {
            var obj = await _userManager.FindByIdAsync(userId);

            if (obj == null)
            {
                return ServiceDataResponse<UserDTO>.Fail("User not founded!");
            }

            var _passwordValidator =
                httpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
            var _passwordHasher =
                httpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            IdentityResult result =
                await _passwordValidator.ValidateAsync(_userManager, obj, newPass);

            if (result.Succeeded)
            {
                obj.PasswordHash = _passwordHasher.HashPassword(obj, newPass);
                await _userManager.UpdateAsync(obj);
                var objDTO = _mapper.Map<UserDTO>(obj);

                return ServiceDataResponse<UserDTO>.Success(objDTO);
            }
            else
            {
                var errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description); //logger
                }
                return ServiceDataResponse<UserDTO>.Fail("User not founded!");
            }
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class UserManageService : IUserManageService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserManageService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get all users
        /// </summary>
        public async Task<ServiceDataResponse<List<UserDTO>>> GetUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();

            if (result == null)
                return ServiceDataResponse<List<UserDTO>>.Fail("There are no users exist"); //Success?

            var objDTO = _mapper.Map<List<UserDTO>>(result);

            return ServiceDataResponse<List<UserDTO>>.Success(objDTO);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        public async Task<ServiceDataResponse<UserDTO>> CreateUserAsync(string userName, string pass)
        {
            try
            {
                User user = new User { Email = userName, UserName = userName, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, pass);
                await _userManager.AddToRoleAsync(user, "user");
                var objDTO = _mapper.Map<UserDTO>(user);
                return ServiceDataResponse<UserDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<UserDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        public async Task<ServiceResponse> DeleteUserAsync(string userId)
        {
            var obj = await _userManager.FindByIdAsync(userId);

            if (obj == null)
            {
                return ServiceResponse.Fail("User not founded!");
            }

            try
            {
                await _userManager.DeleteAsync(obj);
                return ServiceResponse.Success();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public UserService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get user by UserName
        /// </summary>
        public async Task<ServiceDataResponse<List<UserDTO>>> GetUsersAsync(string search)
        {
            var obj = await _webDiaryContext.Users.Where(p => p.UserName.Contains(search)).ToListAsync();

            if (obj == null)
                return ServiceDataResponse<List<UserDTO>>.Fail("User is not exist!");

            var objDTO = _mapper.Map<List<UserDTO>>(obj);

            return ServiceDataResponse<List<UserDTO>>.Success(objDTO);
        }
    }
}

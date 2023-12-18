using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        public GroupService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Loading all groups of an authenticated user, where he is a member
        /// </summary>
        /// <param name="id">
        /// ID of an authenticated user
        /// </param>
        public async Task<ServiceDataResponse<List<GroupDTO>>> ShowUserGroups(string id)
        {
            var userGroups = await _webDiaryContext.Users.Where(u => u.Id == id).SelectMany(g => g.Groups).ToListAsync();

            if (userGroups.Count == 0)
                return ServiceDataResponse<List<GroupDTO>>.Fail("There are no events!");

            var userGroupsDTO = _mapper.Map<List<GroupDTO>>(userGroups);

            return ServiceDataResponse<List<GroupDTO>>.Success(userGroupsDTO);
        }
    }
}

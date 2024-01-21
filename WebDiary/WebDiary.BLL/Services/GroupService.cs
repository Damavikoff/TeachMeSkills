using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;
using static System.Net.Mime.MediaTypeNames;

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
        /// Create a new group
        /// </summary>
        public async Task<ServiceResponse> CreateGroupAsync(GroupDTO groupModel, string authUserId)
        {
            if (authUserId == null)
                return ServiceResponse.Fail("You are not authenticated!");
            groupModel.Id = Guid.NewGuid();
            //var test = _webDiaryContext.Users.FirstOrDefault(sc => sc.UserName == "test3@mail.ru");
            //_webDiaryContext.Entry(test).State = EntityState.Unchanged;
            var obj = _mapper.Map<Group>(groupModel);
            foreach (var r in obj.Users)
            {
                _webDiaryContext.Users.Attach(r);
            }
            try
            {
                //test.Id = Guid.NewGuid();
                //var obj = _mapper.Map<Group>(groupModel);
                //obj.Users.Add(test);
                _webDiaryContext.Groups.Add(obj);
                await _webDiaryContext.SaveChangesAsync();

                return ServiceResponse.Success("Group successfully created!");
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Loading all groups of an authenticated user, where he is a member
        /// </summary>
        /// <param name="id">
        /// ID of an authenticated user
        /// </param>
        public async Task<ServiceDataResponse<List<GroupDTO>>> ShowUserGroups(string id)
        {
            var userGroups = await _webDiaryContext.Users.Where(u => u.Id == id).SelectMany(g => g.JoinedGroups).ToListAsync();

            if (userGroups.Count == 0)
                return ServiceDataResponse<List<GroupDTO>>.Fail("There are no groups!");

            var userGroupsDTO = _mapper.Map<List<GroupDTO>>(userGroups);

            return ServiceDataResponse<List<GroupDTO>>.Success(userGroupsDTO);
        }

        public Task<ServiceResponse> UpdateGroupAsync(GroupDTO groupModel, string authUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteGroupAsync(Guid groupId, string authUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDataResponse<GroupDTO>> GetGroupAsync(Guid groupId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

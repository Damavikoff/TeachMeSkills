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
            var userGroups = await _webDiaryContext.Users.Where(u => u.Id == id).SelectMany(g => g.JoinedGroups).ToListAsync();

            if (userGroups.Count == 0)
                return ServiceDataResponse<List<GroupDTO>>.Fail("There are no groups!");

            var userGroupsDTO = _mapper.Map<List<GroupDTO>>(userGroups);

            return ServiceDataResponse<List<GroupDTO>>.Success(userGroupsDTO);
        }

        /// <summary>
        /// Get group by ID
        /// </summary>
        public async Task<ServiceDataResponse<GroupDTO>> GetGroupAsync(Guid groupId, string userId)
        {
            var obj = await _webDiaryContext.Groups.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == groupId);

            if (obj == null)
                return ServiceDataResponse<GroupDTO>.Fail("Group is not founded!");

            var objDTO = _mapper.Map<GroupDTO>(obj);

            return ServiceDataResponse<GroupDTO>.Success(objDTO);
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        public async Task<ServiceDataResponse<GroupDTO>> CreateGroupAsync(GroupDTO groupModel, string authUserId)
        {
            if (authUserId == null)
                return ServiceDataResponse<GroupDTO>.Fail("You are not authenticated!");

            groupModel.Id = Guid.NewGuid();
            
            var obj = _mapper.Map<Group>(groupModel);

            //add auth user to new group
            var creator = _webDiaryContext.Users.FirstOrDefault(sc => sc.Id == authUserId);

            if (creator != null)
            {
                obj.Users.Add(creator);
            }
            try
            {
                foreach (var r in obj.Users)
                {
                    _webDiaryContext.Users.Attach(r);
                }

                _webDiaryContext.Groups.Add(obj);
                await _webDiaryContext.SaveChangesAsync();
                var objDTO = _mapper.Map<GroupDTO>(groupModel);
                return ServiceDataResponse<GroupDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<GroupDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Update existing group 
        /// </summary>
        public async Task<ServiceDataResponse<GroupDTO>> UpdateGroupAsync(GroupDTO groupModel, string authUserId)
        {
            var obj = await _webDiaryContext.Groups
                                             .Include(p => p.Users)
                                             .FirstOrDefaultAsync(p => p.Id == groupModel.Id);

            if (obj.UserId != authUserId)
            {
                return ServiceDataResponse<GroupDTO>.Fail("You can not update this group!");
            }

            //some check for users in obj and in groupModel?
            //func for cannot delete group creator
            if (obj.Users != null)
            {
                obj.Users.Clear();
            }

            try
            {
                var availableUsers = await _webDiaryContext.Users.ToListAsync();

                foreach (var user in groupModel.Users)
                {
                    obj.Users.Add(availableUsers.First(p => p.Id == user.Id)); // do i need availableUsers?
                }

                obj.Name = groupModel.Name;
                //func for change group creator

                await _webDiaryContext.SaveChangesAsync();

                var objDTO = _mapper.Map<GroupDTO>(obj);
                return ServiceDataResponse<GroupDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<GroupDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete existing group 
        /// </summary>
        public async Task<ServiceResponse> DeleteGroupAsync(Guid groupId, string authUserId)
        {
            var obj = await _webDiaryContext.Groups.FirstOrDefaultAsync(p => p.Id == groupId);

            if (obj == null)
            {
                return ServiceResponse.Fail("Group is not founded!");
            }

            if (obj.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not delete this group!");
            }

            try
            {
                _webDiaryContext.Groups.Remove(obj);
                await _webDiaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
            return ServiceResponse.Success("Group successfully deleted!");
        }
    }
}
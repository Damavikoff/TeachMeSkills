using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        public FriendsService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get friends of auth user
        /// </summary>
        public async Task<ServiceDataResponse<List<FriendsDTO>>> GetFriendsAsync(string authUserId)
        {
            var objs = await _webDiaryContext.Friends.Include(x => x.Friend).Where(a => a.UserId == authUserId).ToListAsync();

            if (objs == null)
                return ServiceDataResponse<List<FriendsDTO>>.Fail("Friends is not exist!");

            var objDTO = _mapper.Map<List<FriendsDTO>>(objs);

            return ServiceDataResponse<List<FriendsDTO>>.Success(objDTO);
        }

        /// <summary>
        /// Get friends of auth user which starts with search string from client
        /// </summary>
        public async Task<ServiceDataResponse<List<FriendsDTO>>> GetFriendsBySearchAsync(string search, string authUserId)
        {
            var friends = await _webDiaryContext.Friends.Include(x => x.Friend).Where(a => a.UserId == authUserId).ToListAsync();

            var objs = await _webDiaryContext.Users.Where(u => u.Id == authUserId).SelectMany(g => g.UserFriends).Where(p => p.Friend.UserName.StartsWith(search)).ToListAsync();

            if (objs == null)
                return ServiceDataResponse<List<FriendsDTO>>.Fail("User is not exist!");

            var result = _mapper.Map<List<FriendsDTO>>(objs);

            return ServiceDataResponse<List<FriendsDTO>>.Success(result);
        }

        /// <summary>
        /// Get friend of auth user by friend id
        /// </summary>
        public async Task<ServiceDataResponse<FriendsDTO>> GetFriendAsync(string friendId, string authUserId)
        {
            var obj = await _webDiaryContext.Friends.Include(x => x.Friend).Where(a => a.UserId == authUserId).FirstOrDefaultAsync(a => a.FriendId == friendId);

            if (obj == null)
                return ServiceDataResponse<FriendsDTO>.Fail("Friend is not founded!");

            var objDTO = _mapper.Map<FriendsDTO>(obj);

            return ServiceDataResponse<FriendsDTO>.Success(objDTO);
        }

        /// <summary>
        /// Add friend to auth user
        /// </summary>
        public async Task<ServiceDataResponse<FriendsDTO>> AddFriendAsync(FriendsDTO friendsModel, string authUserId)
        {
            if (authUserId == null)
                return ServiceDataResponse<FriendsDTO>.Fail("You are not authenticated!");

            try
            {
                var obj = _mapper.Map<Friends>(friendsModel);
                _webDiaryContext.Friends.Add(obj);
                await _webDiaryContext.SaveChangesAsync();
                var objDTO = _mapper.Map<FriendsDTO>(friendsModel);
                return ServiceDataResponse<FriendsDTO>.Success(objDTO);
            }
            catch (Exception ex)
            {
                return ServiceDataResponse<FriendsDTO>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete friend of auth user by friend id
        /// </summary>
        public async Task<ServiceResponse> DeleteFriendAsync(string friendId, string authUserId)
        {
            var obj = await _webDiaryContext.Friends.Where(u => u.UserId == authUserId).FirstOrDefaultAsync(p => p.FriendId == friendId);

            if (obj == null)
            {
                return ServiceResponse.Fail("Friend is not founded!");
            }

            if (obj.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not delete this friend!");
            }

            try
            {
                _webDiaryContext.Friends.Remove(obj);
                await _webDiaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }

            return ServiceResponse.Success("Friend successfully deleted!");
        }
    }
}
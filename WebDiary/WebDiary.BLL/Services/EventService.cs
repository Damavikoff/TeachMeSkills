using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        public EventService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext ?? throw new ArgumentNullException(nameof(webDiaryContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Loading all events of an authenticated user (including groups where he is a member)
        /// </summary>
        /// <param name="start">
        /// First date in the displayed calendar sheet
        /// </param>
        /// <param name="end">
        /// Last date in the displayed calendar sheet
        /// </param>
        /// <param name="authUserId">
        /// ID of an authenticated user
        /// </param>
        public async Task<ServiceDataResponse<List<EventDTO>>> LoadEventsAsync(DateTime start, DateTime end, string authUserId)
        {
            //select events filtered by userid and date
            var userEvents = await _webDiaryContext.Events.Where(u => u.UserId == authUserId).Where(d => d.Start >= start && d.End <= end).ToListAsync();

            //select groups of user
            var userGroups = await _webDiaryContext.Users.Where(u => u.Id == authUserId).SelectMany(g => g.Groups).ToListAsync();

            if (userEvents.Count == 0 && userGroups.Count == 0)
                return ServiceDataResponse<List<EventDTO>>.Fail("There are no events!"); //this is success operation but with empty response -- for webapi

            if (userGroups.Count == 0)
            {
                var objsDTO = _mapper.Map<List<EventDTO>>(userEvents);

                return ServiceDataResponse<List<EventDTO>>.Success(objsDTO);
            }

            List<Guid?> groupGuids = new List<Guid?>();

            foreach (var userGroup in userGroups)
            {
                groupGuids.Add(userGroup.Id);
            }

            //select all events of user group
            var groupEvents = await _webDiaryContext.Events.Where(u => groupGuids.Contains(u.GroupId)).Where(d => d.Start >= start && d.End <= end).ToListAsync();

            if (groupEvents.Count == 0)
            {
                //if user without personal events but with group without events
                if (userEvents.Count == 0)
                    return ServiceDataResponse<List<EventDTO>>.Fail("There are no events!"); //for webapi should return empty body

                var objsDTO = _mapper.Map<List<EventDTO>>(groupEvents);

                return ServiceDataResponse<List<EventDTO>>.Success(objsDTO);
            }
            else
            {
                var userAndGroupEvents = userEvents.Union(groupEvents).ToList();
                var objsDTO = _mapper.Map<List<EventDTO>>(userAndGroupEvents);

                return ServiceDataResponse<List<EventDTO>>.Success(objsDTO);
            }
        }

        /// <summary>
        /// Get event by ID
        /// </summary>
        public async Task<ServiceDataResponse<EventDTO>> GetEventAsync(Guid eventId, string authUserId)
        {
            //check if can user see this event

            var obj = await _webDiaryContext.Events.Include(p=>p.Group)
                                                   .FirstOrDefaultAsync(p => p.Id == eventId);

            //if (obj.UserId != authUserId)
            //    return ServiceDataResponse<EventDTO>.Fail("You can not get this event!");
            //какой-нибудь чек доступен ли пользователю этот ивент для просмотра

            if (obj == null)
                return ServiceDataResponse<EventDTO>.Fail("Event is not founded!");

            var objDTO = _mapper.Map<EventDTO>(obj);

            return ServiceDataResponse<EventDTO>.Success(objDTO);
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        public async Task<ServiceResponse> CreateEventAsync(EventDTO eventModel, string authUserId)
        {
            if (authUserId == null)
                return ServiceResponse.Fail("You are not authenticated!");

            try
            {
                eventModel.Id = Guid.NewGuid();
                var obj = _mapper.Map<Event>(eventModel);
                _webDiaryContext.Events.Add(obj);
                await _webDiaryContext.SaveChangesAsync(); //request to DB is here => async

                return ServiceResponse.Success("Event successfully created!");
            }
            catch(Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Update existing event 
        /// </summary>
        public async Task<ServiceResponse> UpdateEventAsync(EventDTO eventModel, string authUserId)
        {
            if (eventModel.UserId != authUserId) 
            {
                return ServiceResponse.Fail("You can not update this event!");
            }

            try
            {
                var obj = _mapper.Map<Event>(eventModel);
                _webDiaryContext.Events.Update(obj);
                await _webDiaryContext.SaveChangesAsync();

                return ServiceResponse.Success("Event successfully updated!");
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Delete existing event 
        /// </summary>
        public async Task<ServiceResponse> DeleteEventAsync(Guid eventId, string authUserId)
        {
            var obj = await _webDiaryContext.Events.FirstOrDefaultAsync(p => p.Id == eventId);

            if (obj.UserId != authUserId)
            {
                return ServiceResponse.Fail("You can not delete this event!");
            }

            if (obj == null)
            {
                return ServiceResponse.Fail("Event is not founded!");
            }

            _webDiaryContext.Events.Remove(obj);
            await _webDiaryContext.SaveChangesAsync();

            return ServiceResponse.Success("Event successfully deleted!");
        }
    }
}
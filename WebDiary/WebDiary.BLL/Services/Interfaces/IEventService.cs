using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IEventService
    {
        Task<ServiceDataResponse<List<EventDTO>>> LoadEventsAsync(DateTime start, DateTime end, string authUserId);
        Task<ServiceDataResponse<EventDTO>> GetEventAsync(Guid eventId, string userId);
        Task<ServiceDataResponse<EventDTO>> CreateEventAsync(EventDTO eventModel, string authUserId);
        Task<ServiceDataResponse<EventDTO>> UpdateEventAsync(EventDTO eventModel, string authUserId);
        Task<ServiceResponse> DeleteEventAsync(Guid eventId, string authUserId);
    }
}
using WebDiary.BLL.Models;
using WebDiary.BLL.Models.ServiceResponses;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IEventService
    {
        Task<ServiceDataResponse<List<EventDTO>>> LoadEventsAsync(DateTime start, DateTime end, string authUserId);
        Task<ServiceDataResponse<EventDTO>> GetEventAsync(Guid eventId, string userId);
        Task<ServiceDataResponse<EventDTO>> CreateEventAsync(EventDTO eventModel, string authUserId); //я ничего не возвращаю, т.к. при добавлении/апдейте/делите у меня повторно загружаются ивенты. 
        Task<ServiceDataResponse<EventDTO>> UpdateEventAsync(EventDTO eventModel, string authUserId); //или надо отдавать объект и на фронте его добавлять?
        Task<ServiceResponse> DeleteEventAsync(Guid eventId, string authUserId);
    }
}
using WebDiary.BLL.Models;

namespace WebDiary.BLL.Services.Interfaces
{
    public interface IEventService
    {
        Task<ServiceDataResponse<List<EventDTO>>> LoadEventsAsync();
        Task<ServiceDataResponse<EventDTO>> GetEventAsync(Guid eventId);
        Task<ServiceResponse> CreateEventAsync(EventDTO eventModel); //я ничего не возвращаю, т.к. при добавлении/апдейте/делите у меня повторно загружаются ивенты. 
        Task<ServiceResponse> UpdateEventAsync(EventDTO eventModel); //или надо отдавать объект и на фронте его добавлять?
        Task<ServiceResponse> DeleteEventAsync(Guid eventId);
    }
}
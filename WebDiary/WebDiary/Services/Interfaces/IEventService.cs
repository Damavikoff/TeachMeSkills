using WebDiary.Models;

namespace WebDiary.Services.Interfaces
{
    public interface IEventService
    {
        List<EventDTO> LoadEvents();
        EventDTO GetEvent(Guid eventId);
        void CreateEvent(EventDTO eventModel);
        void UpdateEvent(EventDTO eventModel);
        void DeleteEvent(Guid eventId);
    }
}

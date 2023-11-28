using WebDiary.Models;

namespace WebDiary.Services
{
    public interface IEventService
    {
        List<Event> GenerateEvents();
        void CreateEvent(Event eventModel);
        void RemoveEvent(Guid eventId);
        Event GetEvent(Guid eventId);
    }
}

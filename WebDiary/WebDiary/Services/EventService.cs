using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebDiary.Models;
namespace WebDiary.Services
{
    public class EventService : IEventService
    {
        WebDiaryContext _webDiaryContext;
        public EventService(WebDiaryContext webDiaryContext)
        {
            _webDiaryContext = webDiaryContext;
        }
        public void CreateEvent(Event eventModel)
        {
            _webDiaryContext.Events.Add(eventModel);
            _webDiaryContext.SaveChanges();
        }
        public void RemoveEvent(Guid eventId) 
        {
            if (eventId != null)
            {
                Event? eventModel = _webDiaryContext.Events.FirstOrDefault(p => p.Id == eventId);
                if (eventModel != null)
                {
                    _webDiaryContext.Events.Remove(eventModel);
                    _webDiaryContext.SaveChanges();
                }
            }
        }
        public List<Event> GenerateEvents()
        {
            var Posts = _webDiaryContext.Events.ToList();
            return Posts;
        }
        public Event GetEvent(Guid eventId)
        {
            Event obj = null;
            obj = _webDiaryContext.Events.FirstOrDefault(p => p.Id == eventId);
            return obj;
        }
    }
}

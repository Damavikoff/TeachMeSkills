namespace WebDiary.Models
{
    public class EventViewModel
    {
        public Guid id { get; set; } //Guid

        public String title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }
        public String description { get; set; }

        public bool allDay { get; set; }
        public String url { get; set; }
        public String backgroundColor { get; set; }
        public String extendedProps { get; set; }
    }
}

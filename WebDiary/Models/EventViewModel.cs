namespace WebDiary.Models
{
    public class EventViewModel
    {
        public Guid id { get; set; } //Guid not null

        public String title { get; set; } // not null

        public DateTime start { get; set; } // not null

        public DateTime end { get; set; } // not null
        public String description { get; set; }

        public bool allDay { get; set; } // not null
        public String url { get; set; }
        public String backgroundColor { get; set; }
    }
}

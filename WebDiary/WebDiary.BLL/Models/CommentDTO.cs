namespace WebDiary.BLL.Models
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public string? UserId { get; set; }

        public UserDTO? User { get; set; }

        public Guid EventId { get; set; }

        public EventDTO Event { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedDate { get; set; } = default(DateTime);
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebDiary.DAL.Models
{
    public partial class Comment
    {
        public Guid Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public User? User { get; set; }

        [Required]
        public Guid? EventId { get; set; }

        [Required]
        public Event? Event { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
    }
}

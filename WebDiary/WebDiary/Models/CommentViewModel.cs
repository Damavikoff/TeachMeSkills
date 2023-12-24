using System.ComponentModel.DataAnnotations;

namespace WebDiary.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$",
        ErrorMessage = "Wrong USERID value")]
        public string? UserId { get; set; }

        //[Required]
        //public UserViewModel? User { get; set; }

        [Required]
        public Guid? EventId { get; set; }

        //[Required]
        //public EventViewModel? Event { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}

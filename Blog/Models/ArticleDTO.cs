using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ArticleDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid AccountId { get; set; }
        //public Account? Account { get; set; }
        public List<CommentDTO> Comments { get; set; } = new();
    }
}
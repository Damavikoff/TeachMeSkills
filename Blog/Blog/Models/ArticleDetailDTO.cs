using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ArticleDetailDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string AccountName { get; set; }
        public List<CommentDTO> Comments { get; set; } = new();
    }
}

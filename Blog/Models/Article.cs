using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Article
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public List<Comment> Comments { get; set; } = new();

        [NotMapped]
        public Comment Comment { get; set; }
    }
}
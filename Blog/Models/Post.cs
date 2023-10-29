using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
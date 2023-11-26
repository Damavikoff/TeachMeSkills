namespace Blog.Models
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AccountId { get; set; }
        public Guid ArticleId { get; set; }
    }
}
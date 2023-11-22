namespace Blog.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public Guid ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
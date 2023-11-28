namespace Blog.Models
{
    public class CommentDetailDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AccountName { get; set; }
        public string ArticleName { get; set; }
    }
}

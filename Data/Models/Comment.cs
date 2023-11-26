namespace Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }
        public Guid ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
namespace Data.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
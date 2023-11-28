namespace Blog.Models
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<ArticleDTO> Articles { get; set; } = new();
        public List<CommentDTO> Comments { get; set; } = new();
    }
}

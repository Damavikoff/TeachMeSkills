namespace Data.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}

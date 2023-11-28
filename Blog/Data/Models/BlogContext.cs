using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Account> Accounts => Set<Account>();
        public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
        {
        }
    }
}
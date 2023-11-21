using Microsoft.EntityFrameworkCore;

namespace Lesson26.Models
{
    public class SparjaContext : DbContext
    {
        public DbSet<Sparja> Sparjas => Set<Sparja>();
        public SparjaContext() => Database.EnsureCreated();
        public SparjaContext(DbContextOptions<SparjaContext> options)
        : base(options)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace WebDiary.Models;

public partial class WebDiaryContext : DbContext
{
    public WebDiaryContext()
    {
    }

    public WebDiaryContext(DbContextOptions<WebDiaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("connectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(e => e.AllDay);
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.End)
                .HasColumnType("datetime");
            entity.Property(e => e.Id);
            entity.Property(e => e.Start)
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Url)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

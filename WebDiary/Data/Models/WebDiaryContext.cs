using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class WebDiaryContext : DbContext
{

    public WebDiaryContext(DbContextOptions<WebDiaryContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Event> Events { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(e => e.AllDay);
            entity.Property(e => e.BackgroundColor)
                .HasMaxLength(10)
                .IsUnicode(true);
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(true);
            entity.Property(e => e.End)
                .HasColumnType("datetime");
            entity.Property(e => e.Id);
            entity.Property(e => e.Start)
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(true);
            entity.Property(e => e.Url)
                .IsUnicode(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

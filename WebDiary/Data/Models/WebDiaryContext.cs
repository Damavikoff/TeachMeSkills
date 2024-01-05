using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebDiary.DAL.Models;

public partial class WebDiaryContext : IdentityDbContext<User>
{

    public WebDiaryContext(DbContextOptions<WebDiaryContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
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

        modelBuilder.Entity<Group>()
        .HasMany(e => e.Users)
        .WithMany(e => e.Groups)
        .UsingEntity(
            "UserGroups",
            l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(User.Id)),
            r => r.HasOne(typeof(Group)).WithMany().HasForeignKey("GroupsId").HasPrincipalKey(nameof(Group.Id)),
            j => j.HasKey("UsersId", "GroupsId"));

        modelBuilder.Entity<User>()
        .HasMany(e => e.Events)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .IsRequired();

        modelBuilder.Entity<Group>()
        .HasMany(e => e.Events)
        .WithOne(e => e.Group)
        .HasForeignKey(e => e.GroupId);
        //.IsRequired();

        modelBuilder.Entity<Comment>()
        .HasOne(p => p.Event).WithMany(b => b.Comments)
        .HasForeignKey(p => p.EventId)
        .OnDelete(DeleteBehavior.Cascade); //!

        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

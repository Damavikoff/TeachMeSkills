using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    public virtual DbSet<Friends> Friends { get; set; }
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

        modelBuilder.Entity<Event>()
        .ToTable(tb => tb.HasTrigger("trg_EventDelete"));

        modelBuilder.Entity<Comment>()
        .HasOne(p => p.Event).WithMany(b => b.Comments)
        .HasForeignKey(p => p.EventId)
        .OnDelete(deleteBehavior: DeleteBehavior.ClientCascade); //!

        modelBuilder.Entity<Comment>()
       .ToTable(tb => tb.HasTrigger("trg_CommentDelete"));

        modelBuilder.Entity<Group>()
        .HasMany(e => e.Users)
        .WithMany(e => e.JoinedGroups)
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

        modelBuilder.Entity<User>()
        .HasMany(e => e.CreatedGroups)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .OnDelete(deleteBehavior: DeleteBehavior.NoAction) //!
        .IsRequired();

        modelBuilder.Entity<User>()
       .ToTable(tb => tb.HasTrigger("trg_AspNetUsersDelete"));

        modelBuilder.Entity<Group>()
        .HasMany(e => e.Events)
        .WithOne(e => e.Group)
        .HasForeignKey(e => e.GroupIdentificator)
        .OnDelete(deleteBehavior: DeleteBehavior.SetNull);

        modelBuilder.Entity<Group>()
        .ToTable(tb => tb.HasTrigger("trg_GroupDelete"));

        modelBuilder.Entity<Friends>()
               .HasKey(t => new { t.UserId, t.FriendId });

        modelBuilder.Entity<Friends>()
        .ToTable(tb => tb.HasTrigger("trg_FriendsDelete"));

        modelBuilder.Entity<Friends>()
        .HasOne(e => e.User)
        .WithMany(e => e.UserFriends)
        .HasForeignKey(e => e.UserId)
        .IsRequired();

        modelBuilder.Entity<Friends>()
        .HasOne(e => e.Friend)
        .WithMany(e => e.FriendlyUsers)
        .HasForeignKey(e => e.FriendId)
        .OnDelete(deleteBehavior: DeleteBehavior.NoAction) 
        .IsRequired();


        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
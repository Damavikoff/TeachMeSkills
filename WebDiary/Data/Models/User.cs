using Microsoft.AspNetCore.Identity;

namespace WebDiary.DAL.Models
{
    public partial class User : IdentityUser 
    {
        public ICollection<Group> JoinedGroups { get; } //= new();
        public ICollection<Group> CreatedGroups { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Comment> Comments { get; set; } 
        public ICollection<Friends> UserFriends { get; set; } 
        public ICollection<Friends> FriendlyUsers { get; set; } 
    }
}
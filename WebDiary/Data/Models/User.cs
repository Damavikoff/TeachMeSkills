using Microsoft.AspNetCore.Identity;

namespace WebDiary.DAL.Models
{
    public partial class User : IdentityUser 
    {
        public ICollection<Group> Groups { get; } //= new();
        public ICollection<Event> Events { get;  } = new List<Event>();
        public ICollection<Comment> Comments { get; set; } //= new();
    }
}
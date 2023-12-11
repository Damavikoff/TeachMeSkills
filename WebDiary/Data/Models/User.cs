using Microsoft.AspNetCore.Identity;

namespace WebDiary.DAL.Models
{
    public partial class User : IdentityUser 
    {
        public List<Group> Groups { get; } = new();
        public ICollection<Event> Events { get; } = new List<Event>();
    }
}
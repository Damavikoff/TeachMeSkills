using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace WebDiary.DAL.Models
{
    public partial class User : IdentityUser 
    {
        public ICollection<Group> JoinedGroups { get; } //= new();
        public ICollection<Group> CreatedGroups { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Comment> Comments { get; set; } //= new();

        //[BindProperty(SupportsGet = true)]
        //public string? SearchString { get; set; }

        //public SelectList? Genres { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? UserNames { get; set; }
    }
}
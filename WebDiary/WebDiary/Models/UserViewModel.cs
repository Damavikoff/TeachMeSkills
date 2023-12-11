using Microsoft.AspNetCore.Identity;

namespace WebDiary.Models
{
    public class UserViewModel : IdentityUser 
    {
        public List<GroupViewModel>? Groups { get; } = new();
        public ICollection<EventViewModel>? Events { get; } = new List<EventViewModel>();
    }
}
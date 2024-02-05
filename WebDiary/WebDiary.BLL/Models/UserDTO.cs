using Microsoft.AspNetCore.Identity;

namespace WebDiary.BLL.Models
{
    public class UserDTO : IdentityUser 
    {
        public List<GroupDTO> Groups { get; } = new();
        public ICollection<EventDTO> Events { get; } = new List<EventDTO>();
    }
}
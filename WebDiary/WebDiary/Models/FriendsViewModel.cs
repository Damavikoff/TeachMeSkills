using WebDiary.DAL.Models;

namespace WebDiary.Models
{
    public class FriendsViewModel
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        //public User? Friend { get; set; }
        public string? FriendUserName { get; set; } = null!;
    }
}

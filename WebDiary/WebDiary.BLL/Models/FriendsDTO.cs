using WebDiary.DAL.Models;

namespace WebDiary.BLL.Models
{
    public class FriendsDTO
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public User User { get; set; }
        public User Friend { get; set; }
    }
}

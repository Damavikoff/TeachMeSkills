using WebDiary.DAL.Models;

namespace WebDiary.BLL.Models
{
    public class FriendsDTO
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public UserDTO User { get; set; }
        public UserDTO Friend { get; set; }
    }
}

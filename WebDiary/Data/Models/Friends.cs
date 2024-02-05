namespace WebDiary.DAL.Models
{
    public class Friends
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public User User { get; set; }
        public User Friend { get; set; }
    }
}
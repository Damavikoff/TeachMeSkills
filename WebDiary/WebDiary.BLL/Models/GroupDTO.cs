namespace WebDiary.BLL.Models

{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public List<UserDTO> Users { get; set; } = new();
        public ICollection<EventDTO> Events { get; } = new List<EventDTO>();
    }
}
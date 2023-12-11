namespace WebDiary.BLL.Models

{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserDTO> Users { get; } = new();
        public ICollection<EventDTO> Events { get; } = new List<EventDTO>();
    }
}
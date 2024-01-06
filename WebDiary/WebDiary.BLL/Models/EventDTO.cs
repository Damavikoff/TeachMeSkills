namespace WebDiary.BLL.Models;
public class EventDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Description { get; set; }
    public bool AllDay { get; set; }
    public string? Url { get; set; }
    public string? BackgroundColor { get; set; }
    public string? LastBackgroundColor { get; set; }
    public string UserId { get; set; }
    public Guid? GroupId { get; set; }
    public GroupDTO? Group { get; set; } = null!;
    public bool? IsDone { get; set; } = false;
}
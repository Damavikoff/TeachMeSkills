using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public partial class Event
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? Description { get; set; }

    public bool AllDay { get; set; }

    public string? Url { get; set; } = " ";

    public string? BackgroundColor { get; set; }
}

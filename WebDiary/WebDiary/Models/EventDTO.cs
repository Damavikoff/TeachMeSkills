using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.Models;

public partial class EventDTO
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    [Required]
    public string? Description { get; set; }

    public bool AllDay { get; set; }

    public string? Url { get; set; }

    public string? BackgroundColor { get; set; }
}
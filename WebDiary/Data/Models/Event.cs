﻿using System.ComponentModel.DataAnnotations;

namespace WebDiary.DAL.Models;

public partial class Event
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? Description { get; set; }

    public bool AllDay { get; set; }

    public string? Url { get; set; }

    public string? BackgroundColor { get; set; }
    public string UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
}
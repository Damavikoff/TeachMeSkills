using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.Models;

public class EventViewModel
{
    [Key]
    [HiddenInput(DisplayValue = false)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    //min value not need because Required not pass "" and null
    public string Title { get; set; } = null!;

    [Required]
    //[DataType(DataType.Date, ErrorMessage = "Wrong START datetime value")]
    [Range(typeof(DateTime), "1/1/1950", "1/1/2500", 
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public DateTime Start { get; set; }

    [Required]
    [Range(typeof(DateTime), "1/1/1950", "1/1/2500", 
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    //end should be > that start
    public DateTime End { get; set; }

    [StringLength(150,
        ErrorMessage = "DESCRIPTION can contain a maximum of 150 characters")]
    public string? Description { get; set; }

    [Required]
    public bool AllDay { get; set; }

    //[Url] annotation worked at empty field
    [RegularExpression("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)", 
        ErrorMessage = "Wrong URL value")]
    public string? Url { get; set; }

    [Required]
    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", 
        ErrorMessage = "Wrong PRIORITY value")]
    public string? BackgroundColor { get; set; }

    [Required]
    [RegularExpression("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$",
        ErrorMessage = "Wrong USERID value")]
    public string UserId { get; set; }

    public Guid? GroupId { get; set; } = null!;// nullable for adding null events
    //public GroupViewModel? Group { get; set; } = null!;
}
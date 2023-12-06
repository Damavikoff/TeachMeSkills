using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebDiary.Models;

public partial class EventDTO
{
    [Key]
    [HiddenInput(DisplayValue = false)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    //[DataType(DataType.Date, ErrorMessage = "Wrong START datetime value")]
    //between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 
    //[RegularExpression("(\\d{2}).(\\d{2}).(\\d{4}) (\\d{1}):(\\d{2}):(\\d{2})", ErrorMessage = "Wrong START datetime value")]
    [Range(typeof(DateTime), "1/1/1950", "1/1/2500",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public DateTime Start { get; set; }

    [Required]
    //[RegularExpression("(\\d{2}).(\\d{2}).(\\d{4}) (\\d{1}):(\\d{2}):(\\d{2})", ErrorMessage = "Wrong END datetime value")]
    [Range(typeof(DateTime), "1/1/1950", "1/1/2500",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    //end should be > that start
    public DateTime End { get; set; }

    [StringLength(150)]
    public string? Description { get; set; }

    [Required]
    public bool AllDay { get; set; }

    //[Url] работает на пустой филд
    [RegularExpression("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)", ErrorMessage = "Wrong URL value")]
    public string? Url { get; set; }

    [Required]
    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Wrong PRIORITY value")]
    public string BackgroundColor { get; set; }
}
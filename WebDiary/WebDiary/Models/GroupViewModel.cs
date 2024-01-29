using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.Models
{
    public class GroupViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$",
        ErrorMessage = "Wrong USERID value")]
        public string UserId { get; set; }
        public List<UserViewModel>? Users { get; set; } = new();
        public ICollection<EventViewModel>? Events { get; } = new List<EventViewModel>();
    }
}
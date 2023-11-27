using System.ComponentModel.DataAnnotations;

namespace Lesson26.Models
{
    public class Sparja
    {
        [Key]
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime PostedAt {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }
        private DateTime? dateCreated = null;
    }
}
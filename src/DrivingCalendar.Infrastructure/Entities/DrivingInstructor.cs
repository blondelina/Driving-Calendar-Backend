using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("DrivingInstructors")]
    public class DrivingInstructorEntity
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        public string Cnp { get; set; }
    }
}

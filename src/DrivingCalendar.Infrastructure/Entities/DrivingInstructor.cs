using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("DrivingInstructors")]
    public class DrivingInstructorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        public string Cnp { get; set; }
    }
}

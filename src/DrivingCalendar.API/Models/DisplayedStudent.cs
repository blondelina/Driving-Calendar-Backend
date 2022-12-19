using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Models
{
    public class DisplayedStudent
    {
        [Required]
        public int studentId { get; set; }
        [Required]
        public string studentUserName { get; set; }
        [Required]
        public string studentName { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Models
{
    public class RegisterInstructorAccount : RegisterAccountRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}

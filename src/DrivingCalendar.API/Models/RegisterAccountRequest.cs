using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Models
{
    public class RegisterAccountRequest
    {
        [Required]
        public string Username { get; set; }
      
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

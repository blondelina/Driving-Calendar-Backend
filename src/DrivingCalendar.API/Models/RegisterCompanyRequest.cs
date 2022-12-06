using System.ComponentModel.DataAnnotations;


namespace DrivingCalendar.API.Models
{
    public class RegisterCompanyRequest:RegisterAccountRequest
    {
        [Required]
        public string CompanyName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Models
{
    public class Company :IdentityUser<int>
    {
        public string CompanyName { get; set; }
    }
}

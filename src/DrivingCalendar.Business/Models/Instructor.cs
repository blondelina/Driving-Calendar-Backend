using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Models
{
    public class Instructor : IdentityUser<int>
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

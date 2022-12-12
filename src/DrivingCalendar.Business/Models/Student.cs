using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Models
{
    public class Student : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

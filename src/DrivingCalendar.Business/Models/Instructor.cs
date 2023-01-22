using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Models
{
    public class Instructor : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public string FullName 
        { 
            get { return $"{FirstName} {LastName}"; }
        }
    }
}

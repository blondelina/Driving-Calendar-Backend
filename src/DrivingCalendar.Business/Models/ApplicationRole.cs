using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}

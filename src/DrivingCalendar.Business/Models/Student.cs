using Microsoft.AspNetCore.Identity;
using System;

namespace DrivingCalendar.Business.Models
{
    public class Student : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? ExamDate { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}

using System;

namespace DrivingCalendar.API.Models
{
    public class CreateDrivingLessonInstructorRequest
    {
        public int StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

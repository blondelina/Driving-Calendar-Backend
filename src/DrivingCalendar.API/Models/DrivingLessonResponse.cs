using DrivingCalendar.Business.Constants;
using System;

namespace DrivingCalendar.API.Models
{
    public class DrivingLessonResponse
    {
        public int Id { get; set; }

        public StudentResponse Student { get; set; }

        public InstructorResponse Instructor { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DrivingLessonStatus Status { get; set; }
    }
}

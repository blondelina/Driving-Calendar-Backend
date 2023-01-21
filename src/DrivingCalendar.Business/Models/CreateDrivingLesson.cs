using DrivingCalendar.Business.Constants;
using System;

namespace DrivingCalendar.Business.Models
{
    public class CreateDrivingLesson
    {
        public int InstructorId { get; set; }
        public int StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DrivingLessonStatus Status { get; set; }
    }
}

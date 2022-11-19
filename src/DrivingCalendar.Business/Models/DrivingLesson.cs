using DrivingCalendar.Business.Enums;
using System;

namespace DrivingCalendar.Business.Models
{
    public class DrivingLesson
    {
        public int Id { get; set; }

        public string InstructorId { get; set; }

        public string StudentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DrivingLessonStatus InstructorStatus { get; set; }

        public DrivingLessonStatus StudentStatus { get; set; }
    }
}

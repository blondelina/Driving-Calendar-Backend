using System;
using DrivingCalendar.Business.Constants;

namespace DrivingCalendar.Business.Models
{
    public class DrivingLesson
    {
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public int StudentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DrivingLessonStatus InstructorStatus { get; set; }

        public DrivingLessonStatus StudentStatus { get; set; }
    }
}

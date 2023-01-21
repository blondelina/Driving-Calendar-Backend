using System;
using DrivingCalendar.Business.Constants;

namespace DrivingCalendar.Business.Models
{
    public class DrivingLesson
    {
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string InstructorName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DrivingLessonStatus Status { get; set; }
    }
}

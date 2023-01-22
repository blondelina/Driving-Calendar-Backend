using System;
using DrivingCalendar.Business.Constants;

namespace DrivingCalendar.Business.Models
{
    public class DrivingLesson
    {
        public int Id { get; set; }

        public Instructor Instructor { get; set; }

        public Student Student { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DrivingLessonStatus Status { get; set; }
    }
}

using DrivingCalendar.Business.Constants;
using System;
using System.Collections.Generic;

namespace DrivingCalendar.Business.Models.Filters
{
    public class DrivingLessonFilter
    {
        public IList<int> DrivingLessonIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? InstructorId { get; set; }
        public int? StudentId { get; set; }

        public DrivingLessonStatus? Status { get; set; }
    }
}

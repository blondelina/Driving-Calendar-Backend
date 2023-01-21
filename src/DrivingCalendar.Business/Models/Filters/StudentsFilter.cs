using System.Collections.Generic;

namespace DrivingCalendar.Business.Models.Filters
{
    public class StudentsFilter
    {
        public string SearchString { get; set; }

        public IList<int> InstructorIds { get; set; }

        public IList<int> NotAssignedToInstructorIds { get; set; }
    }
}

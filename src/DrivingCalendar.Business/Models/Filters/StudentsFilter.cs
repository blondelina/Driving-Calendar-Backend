using System.Collections.Generic;

namespace DrivingCalendar.Business.Models.Filters
{
    public class StudentsFilter
    {
        public IList<int> NotAddedToInstructorIds { get; set; } = new List<int>();
    }
}

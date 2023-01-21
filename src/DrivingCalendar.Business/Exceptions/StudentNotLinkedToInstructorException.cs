using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class StudentNotLinkedToInstructorException : CoreException
    {
        public StudentNotLinkedToInstructorException() : base("Student is not added to instructor", HttpStatusCode.BadRequest)
        {
        }
    }
}

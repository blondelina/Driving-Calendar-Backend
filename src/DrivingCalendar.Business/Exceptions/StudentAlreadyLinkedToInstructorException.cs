using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class StudentAlreadyLinkedToInstructorException : CoreException
    {
        private const string ERROR_MESSAGE = "Student is already linked to instructor";
        public StudentAlreadyLinkedToInstructorException() : base(ERROR_MESSAGE, HttpStatusCode.BadRequest)
        {
        }
    }
}

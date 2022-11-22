using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class StudentNotFoundException : CoreException
    {
        private const string ERROR_MESSAGE = "Student was not found";
        public StudentNotFoundException() : base(ERROR_MESSAGE, HttpStatusCode.BadRequest)
        {
        }
    }
}

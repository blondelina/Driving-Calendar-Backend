using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class InstructorNotFoundException : CoreException
    {
        private const string ERROR_MESSAGE = "Instructor was not found";
        public InstructorNotFoundException() : base(ERROR_MESSAGE, HttpStatusCode.BadRequest)
        {
        }
    }
}

using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class InstructortNotFoundException : CoreException
    {
        private const string ERROR_MESSAGE = "Instructor was not found";
        public InstructortNotFoundException() : base(ERROR_MESSAGE, HttpStatusCode.BadRequest)
        {
        }
    }
}

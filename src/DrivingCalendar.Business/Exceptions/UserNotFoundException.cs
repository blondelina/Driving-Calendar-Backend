using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class UserNotFoundException : CoreException
    {
        public UserNotFoundException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound)
        {
        }
    }
}

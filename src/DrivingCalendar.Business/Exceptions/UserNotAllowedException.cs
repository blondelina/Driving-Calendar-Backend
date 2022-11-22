using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class UserNotAllowedException : CoreException
    {
        public UserNotAllowedException() : base(HttpStatusCode.Unauthorized)
        {
        }
    }
}

using System.Net;

namespace DrivingCalendar.Business.Exceptions
{
    internal class DrivingLessonNotFoundException : CoreException
    {
        private const string ERROR_MESSAGE = "Driving lesson was not found";
        public DrivingLessonNotFoundException() : base(ERROR_MESSAGE, HttpStatusCode.NotFound)
        {
        }
    }
}

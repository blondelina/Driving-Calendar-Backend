namespace DrivingCalendar.Business.Exceptions
{
    internal class InvalidAvailabilityIntervalException : CoreException
    {
        public InvalidAvailabilityIntervalException(string message) : base(message, System.Net.HttpStatusCode.BadRequest)
        {
        }
    }
}

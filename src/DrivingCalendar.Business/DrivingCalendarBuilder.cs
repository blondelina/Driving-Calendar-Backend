using Microsoft.Extensions.DependencyInjection;

namespace DrivingCalendar.Business
{
    public class DrivingCalendarBuilder
    {
        public DrivingCalendarBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; set; }
    }
}

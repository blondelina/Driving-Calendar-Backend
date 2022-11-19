using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DrivingCalendar.Business
{
    public static class DrivingCalendarServicesExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services, 
            Action<DrivingCalendarBuilder> setup
        )
        {
            DrivingCalendarBuilder builder = new(services);
            setup(builder);

            services.AddTransient<IDrivingLessonService, DrivingLessonService>();

            return services;
        }
    }
}

using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Services;
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
            services.AddTransient<IAvailabilityService, AvailabilityService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IContextService, ContextService>();

            return services;
        }
    }
}

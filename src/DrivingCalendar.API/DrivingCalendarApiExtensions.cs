using System;
using DrivingCalendar.API.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingCalendar.API
{
    public static class DrivingCalendarApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, Action<ApiOptions> setupOptions)
        {
            ApiOptions options = new();
            setupOptions.Invoke(options);

            services.AddTransient(_ => options);
            return services;
        }
    }
}

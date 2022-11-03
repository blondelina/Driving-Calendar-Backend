using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DrivingCalendar.API.Host.Extensions
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsLocal(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment("Local");
        }
    }
}

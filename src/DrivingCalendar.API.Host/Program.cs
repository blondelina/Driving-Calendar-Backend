using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace DrivingCalendar.API.Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            Startup startup = new(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();
            await startup.Configure(app);
        }
    }
}
using DrivingCalendar.API.Host.Extensions;
using DrivingCalendar.API.Host.Settings;
using DrivingCalendar.Business;
using DrivingCalendar.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DrivingCalendar.API.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationSettings settings = _configuration.Get<ApplicationSettings>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

           
            services.AddServices(builder => builder.PersistInSqlServer(settings.ConnectionStrings.DefaultConnection));
        }

        public async Task Configure(WebApplication app)
        {
            if (app.Environment.IsLocal() || app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                await DrivingCalendarBuilderExtensions.RunMigrationsAsync(scope.ServiceProvider);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

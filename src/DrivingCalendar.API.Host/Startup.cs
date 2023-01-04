using DrivingCalendar.API.Host.Extensions;
using DrivingCalendar.API.Host.Filters;
using DrivingCalendar.API.Host.Settings;
using DrivingCalendar.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using DrivingCalendar.Business;
using Microsoft.AspNetCore.Identity;
using DrivingCalendar.Business.Configuration;

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

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenWithBearerAuthorization();

            services.AddJwtAuthentication(settings.JwtSettings);

            services.AddIdentityCore<IdentityUser<int>>()
                .AddSignInManager()
                .AddIdentityFrameworkStores();

            services.ConfigureStudentsIdentity()
                .AddIdentityFrameworkStores();
            services.ConfigureInstructorsIdentity()
                .AddIdentityFrameworkStores();
            services.ConfigureCompanyIdentity()
                .AddIdentityFrameworkStores();
            services
                .AddApi(options =>
                {
                    options.JwtIssuer = settings.JwtSettings.Issuer;
                    options.JwtAudience = settings.JwtSettings.Audience;
                    options.JwtSecretKey = settings.JwtSettings.SecretKey;
                    options.JwtExpirationInHours = settings.JwtSettings.ExpirationInHours;
                })
                .AddServices(builder => builder.PersistInSqlServer(settings.ConnectionStrings.DefaultConnection));
        }

        public async Task Configure(WebApplication app)
        {
            if (app.Environment.IsLocal() || app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (IServiceScope scope = app.Services.CreateScope())
            {
                await DrivingCalendarBuilderExtensions.RunMigrationsAsync(scope.ServiceProvider);
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

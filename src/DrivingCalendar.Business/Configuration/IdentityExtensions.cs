using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingCalendar.Business.Configuration
{
    public static class IdentityExtensions
    {
        public static IdentityBuilder ConfigureStudentsIdentity(this IServiceCollection services)
        {
            return services.AddIdentityCore<Student>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;

                    options.Lockout.MaxFailedAccessAttempts = 100;
                })
                .AddSignInManager<SignInManager<Student>>();
        }

        public static IdentityBuilder ConfigureInstructorsIdentity(this IServiceCollection services)
        {
            return services.AddIdentityCore<Instructor>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;

                    options.Lockout.MaxFailedAccessAttempts = 100;
                })
                .AddSignInManager<SignInManager<Instructor>>();
        }
    }
}

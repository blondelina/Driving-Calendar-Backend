using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Services
{
    internal class ContextService : IContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public ContextService(IHttpContextAccessor contextAccessor, UserManager<IdentityUser<int>> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task<IdentityUser<int>> GetCurrentUserAsync()
        {
            var userId = _contextAccessor.HttpContext?.User.Identity?.Name;
            if (userId is null)
            {
                return null;
            }

            IdentityUser<int> currentUser = await _userManager.FindByIdAsync(userId);
            return currentUser;
        }
    }
}

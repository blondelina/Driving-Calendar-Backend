using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IContextService
    {
        Task<IdentityUser<int>> GetCurrentUserAsync();
    }
}

using DrivingCalendar.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IAvailabityService
    {
        Task<IList<int>> CreateAvailabilityAsync(CreateAvailability createAvailability, int repeat);
    }
}

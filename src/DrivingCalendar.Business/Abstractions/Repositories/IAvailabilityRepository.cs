using DrivingCalendar.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IAvailabilityRepository
    {
        Task<int> CreateAvailabilityAsync(CreateAvailability createAvailability);

        Task<IList<int>> CreateManyAsync(IEnumerable<CreateAvailability> createAvailabilities);

        Task<IList<Availability>> GetAvailabilities(int userId, DateTime? startDate, DateTime? endDate);
    }
}

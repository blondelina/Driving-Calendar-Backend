using DrivingCalendar.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IAvailabilityService
    {
        Task<IList<int>> CreateAvailabilityAsync(CreateAvailability createAvailability, int repeat);
        Task<IList<Availability>> GetAvailabilities(int userId, DateTime? startDate, DateTime? endDate);
    }
}

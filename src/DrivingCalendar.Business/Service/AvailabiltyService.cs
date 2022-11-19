using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Service
{
    internal class AvailabiltyService : IAvailabityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabiltyService(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<IList<int>> CreateAvailabilityAsync(CreateAvailability createAvailability, int repeat)
        {
            if(createAvailability.StartDate > createAvailability.EndDate)
            {
                throw new InvalidAvailabilityIntervalException("Availability start date has to be less or equal than end date");
            }

            IList<int> ids = new List<int>();
            for (int i = 0; i <= repeat; i++)
            {
                CreateAvailability currentAvailability = new()
                {
                    UserId = createAvailability.UserId,
                    StartDate = createAvailability.StartDate.AddDays(i * 7),
                    EndDate = createAvailability.EndDate.AddDays(i * 7),
                };
                ids.Add(await _availabilityRepository.CreateAvailabilityAsync(currentAvailability));
            }

            return ids;
        }
    }
}

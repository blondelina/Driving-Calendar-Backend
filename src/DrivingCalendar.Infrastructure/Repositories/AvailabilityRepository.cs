using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.Infrastructure.Repositories
{
    internal class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAvailabilityAsync(CreateAvailability createAvailability)
        {
            AvailabilityEntity availability = new AvailabilityEntity
            {
                UserId = createAvailability.UserId,
                StartDate = createAvailability.StartDate,
                EndDate = createAvailability.EndDate

            };
            await _context.AddAsync(availability);
            await _context.SaveChangesAsync();

            return availability.Id;
        }

        public async Task<IList<int>> CreateManyAsync(IEnumerable<CreateAvailability> createAvailabilities)
        {
            IEnumerable<AvailabilityEntity> availabilityEntities = createAvailabilities.Select(a => new AvailabilityEntity
            {
                UserId = a.UserId,
                StartDate = a.StartDate,
                EndDate = a.EndDate
            });
            foreach(AvailabilityEntity availabilityEntity in availabilityEntities)
            {
                _context.Add(availabilityEntity);
            }
            await _context.SaveChangesAsync();

            return availabilityEntities.Select(e => e.Id).ToList();
        }
    }
}

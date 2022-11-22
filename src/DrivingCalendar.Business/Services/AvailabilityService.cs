using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DrivingCalendar.Business.Services
{
    internal class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IContextService _contextService;

        public AvailabilityService(
            IAvailabilityRepository availabilityRepository,
            IStudentsRepository studentsRepository,
            IInstructorRepository instructorRepository,
            UserManager<IdentityUser<int>> userManager,
            IContextService contextService)
        {
            _availabilityRepository = availabilityRepository;
            _studentsRepository = studentsRepository;
            _instructorRepository = instructorRepository;
            _userManager = userManager;
            _contextService = contextService;
        }

        public async Task<IList<int>> CreateAvailabilityAsync(CreateAvailability createAvailability, int repeat)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != createAvailability.UserId)
            {
                throw new UserNotAllowedException();
            }

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

        public async Task<IList<Availability>> GetAvailabilities(int userId, DateTime? startDate, DateTime? endDate)
        {
            IdentityUser<int> user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new UserNotFoundException();
            }

            IList<Instructor> userInstructors = await _studentsRepository.GetStudentInstructors(userId);
            IList<Student> userStudents = await _instructorRepository.GetInstructorStudents(userId);

            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            IEnumerable<int> usersAllowedToFetchAvailabilities =
                userStudents.Select(s => s.Id).Concat(userInstructors.Select(i => i.Id));
            if (currentUser.Id != userId && !usersAllowedToFetchAvailabilities.Contains(currentUser.Id))
            {
                throw new UserNotAllowedException();
            }

            return await _availabilityRepository.GetAvailabilities(userId, startDate, endDate);
        }
    }
}

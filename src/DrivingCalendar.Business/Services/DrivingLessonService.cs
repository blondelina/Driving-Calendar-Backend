using System.Collections.Generic;
using System.Linq;
using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;
using System.Threading.Tasks;
using DrivingCalendar.Business.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace DrivingCalendar.Business.Services
{
    internal class DrivingLessonService : IDrivingLessonService
    {
        private readonly IDrivingLessonsRepository _drivingLessonsRepository;
        private readonly IContextService _contextService;
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public DrivingLessonService(
            IDrivingLessonsRepository drivingLessonsRepository, 
            IContextService contextService,
            IInstructorRepository instructorRepository, UserManager<IdentityUser<int>> userManager)
        {
            _drivingLessonsRepository = drivingLessonsRepository;
            _contextService = contextService;
            _instructorRepository = instructorRepository;
            _userManager = userManager;
        }

        public async Task<IList<DrivingLesson>> GetByIdAsync()
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            return await _drivingLessonsRepository.GetByIdAsync(currentUser.Id);
        }

        public async Task<int> CreateDrivingLessonByInstructor(CreateDrivingLesson instructorRequest)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorRequest.InstructorId)
            {
                throw new UserNotAllowedException();
            }

            IdentityUser<int> instructor = await _userManager.FindByIdAsync(currentUser.Id.ToString());
            if (instructor == null)
            {
                throw new InstructortNotFoundException();
            }

            IdentityUser<int> student = await _userManager.FindByIdAsync(instructorRequest.StudentId.ToString());
            if (student == null)
            {
                throw new StudentNotFoundException();
            }

            IList<Student> instructorStudentIds =
                await _instructorRepository.GetInstructorStudents(instructorRequest.InstructorId);
            if (instructorStudentIds.All(s => s.Id != instructorRequest.StudentId))
            {
                throw new StudentNotFoundException();
            }


            instructorRequest.InstructorStatus = DrivingLessonStatus.Confirmed;
            instructorRequest.StudentStatus = DrivingLessonStatus.Pending;

             return await _drivingLessonsRepository.CreateDrivingLesson(instructorRequest);
        }
    }
}

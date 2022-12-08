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

        public DrivingLessonService(
            IDrivingLessonsRepository drivingLessonsRepository, 
            IContextService contextService,
            IInstructorRepository instructorRepository)
        {
            _drivingLessonsRepository = drivingLessonsRepository;
            _contextService = contextService;
            _instructorRepository = instructorRepository;
        }

        public async Task<DrivingLesson> GetByIdAsync(int drivingLessonId)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            return await _drivingLessonsRepository.GetByIdAsync(drivingLessonId, currentUser.Id);
        }

        public async Task<(HttpStatusCode, int)> CreateDrivingLessonByInstructor(CreateDrivingLesson instructorRequest)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorRequest.InstructorId)
            {
                throw new UserNotAllowedException();
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

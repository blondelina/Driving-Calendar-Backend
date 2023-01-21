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
using DrivingCalendar.Business.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DrivingCalendar.Business.Services
{
    internal class DrivingLessonService : IDrivingLessonService
    {
        private readonly IDrivingLessonsRepository _drivingLessonsRepository;
        private readonly IContextService _contextService;
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<Instructor> _instructorManager;

        public DrivingLessonService(
            IDrivingLessonsRepository drivingLessonsRepository, 
            IContextService contextService,
            IInstructorRepository instructorRepository, 
            UserManager<Instructor> instructorManager)
        {
            _drivingLessonsRepository = drivingLessonsRepository;
            _contextService = contextService;
            _instructorRepository = instructorRepository;
            _instructorManager = instructorManager;
        }

        public async Task<IList<DrivingLesson>> GetInstructorDrivingLessonsAsync(int instructorId, DateTime? startDate, DateTime? endDate)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if(currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }

            DrivingLessonFilter filter = new()
            {
                InstructorId = instructorId,
                StartDate = startDate,
                EndDate = endDate
            };
            return await _drivingLessonsRepository.GetDrivingLessonsAsync(filter);
        }

        public async Task<DrivingLesson> CreateDrivingLessonByInstructor(CreateDrivingLesson createDrivingLesson)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != createDrivingLesson.InstructorId)
            {
                throw new UserNotAllowedException();
            }

            IdentityUser<int> instructor = await _instructorManager.FindByIdAsync(currentUser.Id.ToString());
            if (instructor == null)
            {
                throw new InstructorNotFoundException();
            }

            IList<Student> instructorStudents = await _instructorRepository.GetInstructorStudents(createDrivingLesson.InstructorId);
            if (!instructorStudents.Any(s => s.Id == createDrivingLesson.StudentId))
            {
                throw new StudentNotLinkedToInstructorException();
            }


            createDrivingLesson.Status = DrivingLessonStatus.Pending; 

             return await _drivingLessonsRepository.CreateDrivingLesson(createDrivingLesson);
        }

        public async Task DeleteDrivingLessonAsync(int drivingLessonId)
        {
            DrivingLessonFilter filter = new()
            {
                DrivingLessonIds = new List<int> { drivingLessonId }
            };
            DrivingLesson drivingLesson = (await _drivingLessonsRepository.GetDrivingLessonsAsync(filter)).FirstOrDefault();

            if(drivingLesson is null)
            {
                throw new DrivingLessonNotFoundException();
            }

            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if(currentUser.Id != drivingLesson.InstructorId)
            {
                throw new UserNotAllowedException();
            }

            await _drivingLessonsRepository.DeleteDrivingLessonAsync(drivingLessonId);
        }
    }
}

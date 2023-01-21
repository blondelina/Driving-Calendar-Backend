using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Services
{
    internal class InstructorService : IInstructorService
    {
        private readonly IContextService _contextService;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IDrivingLessonsRepository _drivingLessonsRepository;
        private readonly UserManager<IdentityUser<int>> _userManager;


        public InstructorService(
            IContextService contextService,
            IInstructorRepository instructorRepository,
            IStudentsRepository studentsRepository,
            IDrivingLessonsRepository drivingLessonsRepository,
            UserManager<IdentityUser<int>> userManager)
        {
            _contextService = contextService;
            _instructorRepository = instructorRepository;
            _studentsRepository = studentsRepository;
            _drivingLessonsRepository = drivingLessonsRepository;
            _userManager = userManager;
        }

        public async Task<int> AddStudentToInstructor(int studentId, int instructorId)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }

            IdentityUser<int> instructor = await _userManager.FindByIdAsync(instructorId.ToString());
            if (instructor == null)
            {
                throw new InstructorNotFoundException();
            }

            IdentityUser<int> student = await _userManager.FindByIdAsync(studentId.ToString());
            if (student == null)
            {
                throw new StudentNotFoundException();
            }


            IList<Student> instructorStudentIds = await _instructorRepository.GetInstructorStudents(instructorId);
            if (instructorStudentIds.Any(s => s.Id == studentId))
            {
                throw new StudentAlreadyLinkedToInstructorException();
            }

            return await _instructorRepository.AddStudent(studentId, instructorId);
        }
        public async Task<IList<Student>> GetStudents(int instructorId, string searchString)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }
            
            IdentityUser<int> instructor = await _userManager.FindByIdAsync(instructorId.ToString());
            if (instructor is null)
            {
                throw new InstructorNotFoundException();
            }

            StudentsFilter filter = new()
            {
                InstructorIds = new[] { currentUser.Id },
                SearchString = searchString
            };
            return await _studentsRepository.GetStudents(filter);

        }
        public async Task<bool> DeletetStudentsFromInstrutor(int studentId, int instructorId)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }

            IdentityUser<int> instructor = await _userManager.FindByIdAsync(instructorId.ToString());
            if (instructor == null)
            {
                throw new InstructorNotFoundException();
            }

            IdentityUser<int> student = await _userManager.FindByIdAsync(studentId.ToString());
            if (student == null)
            {
                throw new StudentNotFoundException();
            }

            DrivingLessonFilter pendingLessonsFilter = new()
            {
                InstructorId = instructorId,
                StudentId = studentId,
                Status = DrivingLessonStatus.Pending
            };
            IList<DrivingLesson> pendingDrivingLessons = await _drivingLessonsRepository.GetDrivingLessonsAsync(pendingLessonsFilter);

            DrivingLessonFilter confirmedFilter = new()
            {
                InstructorId = instructorId,
                StudentId = studentId,
                StartDate = DateTime.Now,
                Status = DrivingLessonStatus.Confirmed
            };
            IList<DrivingLesson> sharedDrivingLessons = await _drivingLessonsRepository.GetDrivingLessonsAsync(confirmedFilter);

            foreach(DrivingLesson drivingLesson in pendingDrivingLessons.Concat(sharedDrivingLessons))
            {
                await _drivingLessonsRepository.UpdateDrivingLessonStatusAsync(drivingLesson.Id, DrivingLessonStatus.Rejected);
            }

            return await _instructorRepository.DeleteStudent(studentId, instructorId);
        }
    }
}

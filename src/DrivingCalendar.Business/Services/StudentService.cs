using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IContextService _contextService;
        private readonly UserManager<Student> _studentManager;

        public StudentService(
            IStudentsRepository studentsRepository,
            IContextService contextService,
            UserManager<Student> studentManager)
        {
            _studentsRepository = studentsRepository;
            _contextService = contextService;
            _studentManager = studentManager;
        }

        public async Task<IList<Student>> GetStudents(StudentsFilter filter)
        {
            return await _studentsRepository.GetStudents(filter);
        }

        public async Task<Student> GetStudentDetailAsync(int studentId)
        {
            Student student = await _studentManager.FindByIdAsync(studentId.ToString());
            if(student == null)
            {
                throw new StudentNotFoundException();
            }
            
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            IList<Instructor> studentInstructors = await _studentsRepository.GetStudentInstructors(studentId);

            if(studentId != currentUser.Id && !studentInstructors.Any(i => i.Id == currentUser.Id))
            {
                throw new UserNotAllowedException();
            }

            return student;
        }

        public async Task UpdateStudentExamDateAsync(int studentId, DateTime? examDate)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if(currentUser.Id != studentId)
            {
                throw new UserNotAllowedException();
            }

            await _studentsRepository.UpdateStudentExamDateAsync(studentId, examDate);
        }
    }
}

using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
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

    
        public InstructorService(IContextService contextService,
        IInstructorRepository instructorRepository)
        {
            _contextService = contextService;
            _instructorRepository = instructorRepository;
        }

        public async Task<int> AddStudentToInstructor(int studentId, int instructorId)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }

            IList<Student> instructorStudentIds = await _instructorRepository.GetInstructorStudents(instructorId);
            if (instructorStudentIds.All(s => s.Id != instructorId))
            {
                throw new StudentNotFoundException();
            }

            return await _instructorRepository.AddStudent(studentId, instructorId);
        }
    }
}

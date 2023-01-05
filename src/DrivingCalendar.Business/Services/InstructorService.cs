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
        private readonly UserManager<IdentityUser<int>> _userManager;


        public InstructorService(IContextService contextService,
        IInstructorRepository instructorRepository,
        UserManager<IdentityUser<int>> userManager)
        {
            _contextService = contextService;
            _instructorRepository = instructorRepository;
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
                throw new InstructortNotFoundException();
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
        public async Task<IList<Student>> GetStudents(int instructorId)
        {
            IdentityUser<int> currentUser = await _contextService.GetCurrentUserAsync();
            if (currentUser.Id != instructorId)
            {
                throw new UserNotAllowedException();
            }
            
            IdentityUser<int> instructor = await _userManager.FindByIdAsync(instructorId.ToString());
            if (instructor is null)
            {
                throw new InstructortNotFoundException();
            }

            return await _instructorRepository.GetInstructorStudents(instructorId);

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
                throw new InstructortNotFoundException();
            }

            IdentityUser<int> student = await _userManager.FindByIdAsync(studentId.ToString());
            if (student == null)
            {
                throw new StudentNotFoundException();
            }
            return await _instructorRepository.DeleteStudent(studentId, instructorId);
       
        }

}
}

using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Exceptions;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<IList<Student>> GetStudents()
        {
            return await _studentsRepository.GetStudents();
        }

        public async Task<IList<Student>> GetStudentsNotAssignedToInstructors(IList<int> instructorIds)
        {
            return await _studentsRepository.GetStudentsNotAssignedToInstructor(instructorIds);
        }
    }
}

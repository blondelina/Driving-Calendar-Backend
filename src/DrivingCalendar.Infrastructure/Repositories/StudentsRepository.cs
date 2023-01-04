using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingCalendar.Infrastructure.Repositories
{
    internal class StudentsRepository: IStudentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Instructor>> GetStudentInstructors(int studentId)
        {
            return await _dbContext.StudentInstructors
                .Where(si => si.StudentId == studentId)
                .Select(si => si.Instructor)
                .ToListAsync();
        }

        public async Task<IList<Student>> GetStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<IList<Student>> GetStudentsNotAssignedToInstructor(IList<int> instructorIds)
        {
            return await _dbContext.Students
                                   .Where(s => !_dbContext.StudentInstructors.Any(si => instructorIds.Contains(si.InstructorId) && si.StudentId == s.Id))
                                   .ToListAsync();
        }
    }
}

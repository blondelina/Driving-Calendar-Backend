using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using DrivingCalendar.Infrastructure.Entities;
using DrivingCalendar.Infrastructure.Extensions;
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

        public async Task<IList<Student>> GetStudents(StudentsFilter filter)
        {
            return await _dbContext.Students
                                   .ApplyFiltering(filter, _dbContext)
                                   .ToListAsync();
        }

        public async Task UpdateStudentExamDateAsync(int studentId, DateTime? examDate)
        {
            Student student = await _dbContext.Students
                                              .Where(s => s.Id == studentId)
                                              .FirstOrDefaultAsync();

            if(student == null)
            {
                return;
            }

            student.ExamDate = examDate;
            await _dbContext.SaveChangesAsync();
        }
    }
}

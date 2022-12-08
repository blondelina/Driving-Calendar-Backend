﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingCalendar.Infrastructure.Repositories
{
    internal class InstructorRepository : IInstructorRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public InstructorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Student>> GetInstructorStudents(int instructorId)
        {
            return await _dbContext.StudentInstructors
                .Where(si => si.InstructorId == instructorId)
                .Select(si => si.Student)
                .ToListAsync();
        }

        public async Task<int> AddStudent(int studentId, int instructorId)
        {
            StudentInstructorEntity studentInstructorEntity = new()
            {
                InstructorId = instructorId,
                StudentId = studentId,
                Student = await _dbContext.Students.FindAsync(studentId),
                Instructor = await _dbContext.Instructors.FindAsync(instructorId)
            };
            _dbContext.Add(studentInstructorEntity);

            return studentInstructorEntity.Id;
        }
    }
}

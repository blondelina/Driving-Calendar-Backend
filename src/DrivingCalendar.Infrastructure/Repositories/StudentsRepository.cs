﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
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
    }
}
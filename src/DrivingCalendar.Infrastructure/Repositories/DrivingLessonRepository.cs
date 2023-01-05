using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Infrastructure.Repositories
{
    internal class DrivingLessonRepository : IDrivingLessonsRepository
    {
        private ApplicationDbContext _context;

        public DrivingLessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IList<DrivingLesson>> GetByIdAsync(int userId)
        {
            return await _context.DrivingLessons.Where(dl => 
                    dl.StudentInstructorEntity.InstructorId == userId || dl.StudentInstructorEntity.StudentId == userId)
                .Select(e => new DrivingLesson
                {
                    Id = e.Id,
                    InstructorId = e.StudentInstructorEntity.InstructorId,
                    StudentId = e.StudentInstructorEntity.StudentId,
                    StudentName = e.StudentInstructorEntity.Student.FirstName+" "+e.StudentInstructorEntity.Student.LastName,
                    InstructorName = e.StudentInstructorEntity.Instructor.FirstName+" "+e.StudentInstructorEntity.Instructor.LastName,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    StudentStatus = e.StudentStatus,
                    InstructorStatus = e.InstructorStatus
                })
                .ToListAsync();
        }

        public async Task<int> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson)
        {
            StudentInstructorEntity studentInstructorEntity = _context.StudentInstructors.Where(dl =>
            dl.InstructorId == createDrivingLesson.InstructorId && dl.StudentId == createDrivingLesson.StudentId)
            .FirstOrDefault();

            DrivingLessonEntity drivingLesson = new()
            {
                StudentInstructorId = studentInstructorEntity.Id,
                StudentInstructorEntity= studentInstructorEntity,
                StartDate = createDrivingLesson.StartDate,
                EndDate = createDrivingLesson.EndDate,
                StudentStatus = createDrivingLesson.StudentStatus,
                InstructorStatus = createDrivingLesson.InstructorStatus,
            };

            _context.Add(drivingLesson);
            await _context.SaveChangesAsync();

            return (drivingLesson.Id);
        }
    }
}

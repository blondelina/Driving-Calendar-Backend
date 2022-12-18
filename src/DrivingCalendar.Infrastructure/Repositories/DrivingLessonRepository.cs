using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<DrivingLesson> GetByIdAsync(int drivingLessonId, int userId)
        {
            return await _context.DrivingLessons.Where(dl => 
                    dl.Id == drivingLessonId 
                    && (dl.StudentInstructorEntity.InstructorId == userId || dl.StudentInstructorEntity.StudentId == userId))
                .Select(e => new DrivingLesson
                {
                    Id = e.Id,
                    InstructorId = e.StudentInstructorEntity.InstructorId,
                    StudentId = e.StudentInstructorEntity.StudentId,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    StudentStatus = e.StudentStatus,
                    InstructorStatus = e.InstructorStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson)
        {

            StudentInstructorEntity studentInstructorEntity = new()
            {
                InstructorId = createDrivingLesson.InstructorId,
                StudentId = createDrivingLesson.StudentId,
                Student = await _context.Students.FindAsync (createDrivingLesson.StudentId),
                Instructor = await _context.Instructors.FindAsync(createDrivingLesson.InstructorId)
            };
            _context.Add(studentInstructorEntity);

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

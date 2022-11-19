using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<DrivingLesson> GetByIdAsync(int drivingLessonId)
        {
            return await _context.DrivingLessons.Where(dl => dl.Id == drivingLessonId)
                .Select(e => new DrivingLesson
                {
                    Id = e.Id,
                    InstructorId = e.InstructorId,
                    StudentId = e.StudentId,
                    StartDate = e.StartDate,
                    EndDate = e.EndTime,
                    StudentStatus = e.StudentStatus,
                    InstructorStatus = e.InstructorStatus
                })
                .FirstOrDefaultAsync();
        }
    }
}

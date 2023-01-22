using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using DrivingCalendar.Infrastructure.Entities;
using DrivingCalendar.Infrastructure.Extensions;
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


        public async Task<IList<DrivingLesson>> GetDrivingLessonsAsync(DrivingLessonFilter filter)
        {
            return await _context.DrivingLessons
                                 .ApplyFiltering(filter)
                                 .Include(dl => dl.Student)
                                 .Include(dl => dl.Instructor)
                                    .ThenInclude(i => i.Company)
                                 .Select(dl => dl.ToDrivingLesson())
                                 .ToListAsync();
        }

        public async Task<DrivingLesson> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson)
        {
            DrivingLessonEntity drivingLesson = new()
            {
                InstructorId = createDrivingLesson.InstructorId,
                StudentId = createDrivingLesson.StudentId,
                StartDate = createDrivingLesson.StartDate,
                EndDate = createDrivingLesson.EndDate,
                Status = createDrivingLesson.Status
            };

            _context.Add(drivingLesson);
            await _context.SaveChangesAsync();

            drivingLesson = _context.DrivingLessons
                                    .Include(dl => dl.Instructor)
                                        .ThenInclude(i => i.Company)
                                    .Include(dl => dl.Student)
                                    .FirstOrDefault(dl => dl.Id == drivingLesson.Id);

            return drivingLesson.ToDrivingLesson();
        }

        public async Task UpdateDrivingLessonStatusAsync(int drivingLessonId, DrivingLessonStatus status)
        {
            DrivingLessonEntity drivingLesson = await _context.DrivingLessons
                                                              .FirstOrDefaultAsync(dl => dl.Id == drivingLessonId);
            if(drivingLesson is null)
            {
                return;
            }

            drivingLesson.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDrivingLessonAsync(int drivingLessonId)
        {
            DrivingLessonEntity drivingLesson = await _context.DrivingLessons.FirstOrDefaultAsync(dl => dl.Id == drivingLessonId);
            if(drivingLesson is not null)
            {
                _context.Remove(drivingLesson);
                await _context.SaveChangesAsync();
            }
        }
    }
}

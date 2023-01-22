using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using DrivingCalendar.Infrastructure.Entities;
using System.Linq;

namespace DrivingCalendar.Infrastructure.Extensions
{
    internal static class DrivingLessonEntityExtensions
    {
        public static IQueryable<DrivingLessonEntity> ApplyFiltering(this IQueryable<DrivingLessonEntity> query, DrivingLessonFilter filter)
        {
            if(filter == null)
            {
                return query;
            }

            if(filter.DrivingLessonIds != null && filter.DrivingLessonIds.Any())
            {
                query = query.Where(dl => filter.DrivingLessonIds.Contains(dl.Id));
            }

            if(filter.StartDate.HasValue)
            {
                query = query.Where(dl => dl.StartDate >= filter.StartDate);
            }
            if(filter.EndDate.HasValue)
            {
                query = query.Where(dl => dl.EndDate <= filter.EndDate);
            }
            if(filter.InstructorId.HasValue)
            {
                query = query.Where(dl => dl.Instructor.Id == filter.InstructorId);
            }
            if (filter.StudentId.HasValue)
            {
                query = query.Where(dl => dl.Student.Id == filter.StudentId);
            }
            if(filter.Status.HasValue)
            {
                query = query.Where(dl => dl.Status == filter.Status);
            }

            return query;
        }

        public static DrivingLesson ToDrivingLesson(this DrivingLessonEntity drivingLessonEntity)
        {
            if(drivingLessonEntity == null)
            {
                return null;
            }

            return new DrivingLesson
            {
                Id = drivingLessonEntity.Id,
                Instructor = drivingLessonEntity.Instructor,
                Student = drivingLessonEntity.Student,
                StartDate = drivingLessonEntity.StartDate,
                EndDate = drivingLessonEntity.EndDate,
                Status = drivingLessonEntity.Status
            };
        }
    }
}

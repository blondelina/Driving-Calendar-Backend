using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IDrivingLessonsRepository
    {
        Task<IList<DrivingLesson>> GetDrivingLessonsAsync(DrivingLessonFilter filter);
        Task<DrivingLesson> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson);
        Task UpdateDrivingLessonStatusAsync(int drivingLessonId, DrivingLessonStatus status);
        Task DeleteDrivingLessonAsync(int drivingLessonId);
    }
}

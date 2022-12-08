using DrivingCalendar.Business.Models;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IDrivingLessonsRepository
    {
        Task<DrivingLesson> GetByIdAsync(int drivingLessonId, int userId);
        Task<(HttpStatusCode, int)> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson);
    }
}

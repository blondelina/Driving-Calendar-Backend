using DrivingCalendar.Business.Models;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IDrivingLessonsRepository
    {
        Task<DrivingLesson> GetByIdAsync(int drivingLessonId, int userId);
        Task<int> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson);
    }
}

using DrivingCalendar.Business.Models;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IDrivingLessonsRepository
    {
        Task<DrivingLesson> GetByIdAsync(int drivingLessongId);
    }
}

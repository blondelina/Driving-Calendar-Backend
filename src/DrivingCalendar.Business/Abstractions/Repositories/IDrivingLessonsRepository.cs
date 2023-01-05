using DrivingCalendar.Business.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IDrivingLessonsRepository
    {
        Task<IList<DrivingLesson>> GetByIdAsync(int userId);
        Task<int> CreateDrivingLesson(CreateDrivingLesson createDrivingLesson);
    }
}

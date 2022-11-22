using DrivingCalendar.Business.Abstractions.Repositories;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Service
{
    internal class DrivingLessonService : IDrivingLessonService
    {
        private readonly IDrivingLessonsRepository _drivingLessonsRepository;

        public DrivingLessonService(IDrivingLessonsRepository drivingLessonsRepository)
        {
            _drivingLessonsRepository = drivingLessonsRepository;
        }

        public async Task<DrivingLesson> GetByIdAsync(int drivingLessonId)
        {
            return await _drivingLessonsRepository.GetByIdAsync(drivingLessonId);
        }
    }
}

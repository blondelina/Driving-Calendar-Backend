using DrivingCalendar.Business.Models;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IDrivingLessonService
    {
        Task<DrivingLesson> GetByIdAsync(int drivingLessonId);
        Task<(HttpStatusCode, int)> CreateDrivingLessonByInstructor(CreateDrivingLesson instructorRequest);
    }
}

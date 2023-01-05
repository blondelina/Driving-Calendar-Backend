using DrivingCalendar.Business.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IDrivingLessonService
    {
        Task<IList<DrivingLesson>> GetByIdAsync();
        Task<int> CreateDrivingLessonByInstructor(CreateDrivingLesson instructorRequest);
    }
}

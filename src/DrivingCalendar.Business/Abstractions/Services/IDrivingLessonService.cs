using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IDrivingLessonService
    {
        Task<IList<DrivingLesson>> GetInstructorDrivingLessonsAsync(int instructorId, DateTime? startDate, DateTime? endDate);
        Task<IList<DrivingLesson>> GetStudentDrivingLessonsAsync(int studentId, DrivingLessonStatus? status);
        Task<DrivingLesson> CreateDrivingLessonByInstructor(CreateDrivingLesson instructorRequest);
        Task PatchDrivingLessonStatus(int drivingLessonId, DrivingLessonStatus status);
        Task DeleteDrivingLessonAsync(int drivingLessonId);
    }
}

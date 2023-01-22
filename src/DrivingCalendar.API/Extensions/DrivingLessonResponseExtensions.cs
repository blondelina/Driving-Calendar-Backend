using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.API.Extensions
{
    internal static class DrivingLessonResponseExtensions
    {
        public static DrivingLessonResponse ToResponse(this DrivingLesson drivingLesson)
        {
            return new DrivingLessonResponse
            {
                Id = drivingLesson.Id,
                Instructor = drivingLesson.Instructor.ToResponse(),
                Student = drivingLesson.Student.ToResponse(),
                StartDate = drivingLesson.StartDate,
                EndDate = drivingLesson.EndDate,
                Status = drivingLesson.Status
            };
        }
    }
}

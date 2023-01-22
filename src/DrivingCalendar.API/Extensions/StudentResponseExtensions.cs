using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.API.Extensions
{
    internal static class StudentResponseExtensions
    {
        public static StudentResponse ToResponse(this Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                Email = student.Email,
                Username = student.UserName,
                Name = student.FullName
            };
        }

        public static StudentDetailResponse ToDetailResponse(this Student student)
        {
            return new StudentDetailResponse
            {
                Id = student.Id,
                Email = student.Email,
                Username = student.UserName,
                Name = student.FullName,
                ExamDate = student.ExamDate
            };
        }
    }
}

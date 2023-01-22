using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.API.Extensions
{
    internal static class InstructorResponseExtensions
    {
        public static InstructorResponse ToResponse(this Instructor instructor)
        {
            return new InstructorResponse
            {
                Id = instructor.Id,
                Email = instructor.Email,
                Username = instructor.UserName,
                Name = instructor.FullName,
                Company = instructor.Company.ToResponse()
            };
        }
    }
}

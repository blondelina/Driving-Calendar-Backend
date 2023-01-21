using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;
using System.Collections.Generic;
using System.Linq;
using DrivingCalendar.Business.Models.Filters;

namespace DrivingCalendar.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("students")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IList<StudentResponse>> GetStudents(
            [FromQuery] int[] notAssignedToInstructors, 
            [FromQuery] string searchString)
        {

            StudentsFilter filter = new()
            {
                SearchString = searchString,
                NotAssignedToInstructorIds = notAssignedToInstructors
            };
            IList<Student> students = await _studentService.GetStudents(filter);

            IList<StudentResponse> response = students.Select(s => new StudentResponse
            {
                Id = s.Id,
                Username = s.UserName,
                Name = $"{s.FirstName} {s.LastName}",
                Email = s.Email,
            }).ToList();

            return response;
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<IList<StudentResponse>> GetStudents([FromQuery] int[] notAssignedToInstructors)
        {
            IList<Student> students;
            if (notAssignedToInstructors?.Any() ?? false)
            {
                students = await _studentService.GetStudentsNotAssignedToInstructors(notAssignedToInstructors);
            }
            else
            {
                students = await _studentService.GetStudents();
            }

            IList<StudentResponse> response = students.Select(s => new StudentResponse
            {
                Id = s.Id,
                Username = s.UserName,
                Name = $"{s.FirstName} {s.LastName}"
            }).ToList();

            return response;
        }


    }
}

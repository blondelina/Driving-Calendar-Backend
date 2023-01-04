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
        public async Task<IList<DisplayedStudent>> GetStudents()
        {
            IList<Student> students = await _studentService.GetStudents();
            IList<DisplayedStudent> response = students.Select(s => new DisplayedStudent
            {
                studentId = s.Id,
                studentUserName = s.UserName,
                studentName = $"{s.FirstName} {s.LastName}"
            }).ToList();

            return response;
        }


    }
}

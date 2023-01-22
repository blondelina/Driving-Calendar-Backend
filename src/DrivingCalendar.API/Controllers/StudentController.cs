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
using DrivingCalendar.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IEnumerable<StudentResponse>> GetStudents(
            [FromQuery] int[] notAssignedToInstructors, 
            [FromQuery] string searchString)
        {

            StudentsFilter filter = new()
            {
                SearchString = searchString,
                NotAssignedToInstructorIds = notAssignedToInstructors
            };
            IList<Student> students = await _studentService.GetStudents(filter);

            return students.Select(s => s.ToResponse());
        }

        [HttpGet("{studentId}/details")]
        [Authorize]
        public async Task<StudentDetailResponse> GetStudentDetailAsync([FromRoute][Required] int studentId)
        {
            Student student = await _studentService.GetStudentDetailAsync(studentId);
            return student.ToDetailResponse();
        }

        [HttpPut("{studentId}/exam-date")]
        [Authorize(Roles = IdentityRoles.STUDENT)]
        public async Task<IActionResult> UpdateStudentExamDateAsync(
            [FromRoute][Required] int studentId,
            [FromBody][Required] UpdateStudentExamDateRequest updateRequest)
        {
            await _studentService.UpdateStudentExamDateAsync(studentId, updateRequest.ExamDate);
            return NoContent();
        }
    }
}

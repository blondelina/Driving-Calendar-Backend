using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DrivingCalendar.API.Controllers
{
    [Route("api/instructors")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost("{instructorId}/students/{studentId}")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IActionResult> AddToInstructor([FromRoute][Required] int instructorId, [FromRoute] int studentId)
        {
            return new ObjectResult(await _instructorService.AddStudentToInstructor(studentId, instructorId));
        }

        [HttpGet("{instructorId}/students")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IList<StudentResponse>> GetStudentsFromInstructors([FromRoute][Required] int instructorId)
        {
            IList<Student> students = await _instructorService.GetStudents(instructorId);
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
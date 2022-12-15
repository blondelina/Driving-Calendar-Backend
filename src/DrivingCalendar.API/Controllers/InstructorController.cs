using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Abstractions.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DrivingCalendar.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost("instructors/{instructorId}/student")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IActionResult> AddToInstructor([FromBody][Required] AddStudentToInstructorRequest request, [FromRoute][Required] int instructorId)
        {
            return new ObjectResult(await _instructorService.AddStudentToInstructor(request.studentId, instructorId));
        }

        [HttpGet("instructors/{instructorId}/students")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IList<StudentResponse>> GetStudentsFromInstructors([FromRoute][Required] int instructorId)
        {
            IList<Student> students = await _instructorService.GetStudents(instructorId);
            IList<StudentResponse> response = students.Select(s => new StudentResponse
            {
                studentId = s.Id,
                studentName = $"{s.FirstName} {s.LastName}"
            }).ToList();
  
            return response;
        }

    }
}
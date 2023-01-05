using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;
using System.Collections.Generic;

namespace DrivingCalendar.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DrivingLessonController : ControllerBase
    {
        private readonly IDrivingLessonService _drivingLessonService;

        public DrivingLessonController(IDrivingLessonService drivingLessonService)
        {
            _drivingLessonService = drivingLessonService;
        }

        [HttpGet("driving-lessons")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR+","+IdentityRoles.STUDENT)]
        public async Task<IList<DrivingLesson>> GetById()
        {
            IList<DrivingLesson> drivingLessons = await _drivingLessonService.GetByIdAsync();
            return drivingLessons;
        }

        [HttpPost("instructors/{instructorId}/driving-lessons")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IActionResult> CreateByInstructor([FromBody][Required] CreateDrivingLessonInstructorRequest instructorRequest, [FromRoute][Required] int instructorId)
        {
            CreateDrivingLesson createDrivingLesson = new()
            {
                StudentId = instructorRequest.StudentId,
                InstructorId = instructorId,
                StartDate = instructorRequest.StartDate,
                EndDate = instructorRequest.EndDate,

            };
            return new ObjectResult(await _drivingLessonService.CreateDrivingLessonByInstructor(createDrivingLesson));

        }
    }
}

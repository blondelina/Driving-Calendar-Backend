using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Constants;

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

        // GET: api/<ValuesController>
        [HttpGet("driving-lessons/{drivingLessonId}")]

        public async Task<ActionResult<DrivingLesson>> GetById([FromRoute] int drivingLessonId)
        {
            DrivingLesson drivingLesson = await _drivingLessonService.GetByIdAsync(drivingLessonId);
            return drivingLesson is not null ? drivingLesson : NotFound();
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

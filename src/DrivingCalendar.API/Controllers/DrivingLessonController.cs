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
using DrivingCalendar.Business.Models.Filters;
using System.Linq;
using DrivingCalendar.API.Extensions;

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

        [HttpGet("instructors/{instructorId}/driving-lessons")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IEnumerable<DrivingLessonResponse>> GetInstructorDrivingLessons(
            [FromRoute][Required] int instructorId, 
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate)
        {
            IList<DrivingLesson> drivingLessons = await _drivingLessonService.GetInstructorDrivingLessonsAsync(instructorId, startDate, endDate);
            return drivingLessons.Select(dl => dl.ToResponse());
        }

        [HttpGet("students/{studentId}/driving-lessons")]
        [Authorize(Roles = IdentityRoles.STUDENT)]
        public async Task<IEnumerable<DrivingLessonResponse>> GetStudentDrivingLessons(
            [FromRoute][Required] int studentId,
            [FromQuery] DrivingLessonStatus? status)
        {
            IList<DrivingLesson> drivingLessons = await _drivingLessonService.GetStudentDrivingLessonsAsync(studentId, status);
            return drivingLessons.Select(dl => dl.ToResponse());
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

            DrivingLesson createdDrivingLesson = await _drivingLessonService.CreateDrivingLessonByInstructor(createDrivingLesson);
            return Created("", createdDrivingLesson.ToResponse());
        }

        [HttpPatch("driving-lessons/{drivingLessonId}")]
        [Authorize(Roles = IdentityRoles.STUDENT)]
        public async Task<IActionResult> PatchDrivingLessonStatus(
            [FromRoute][Required] int drivingLessonId,
            [FromBody][Required] PatchDrivingLessonStatusRequest patchRequest)
        {
            await _drivingLessonService.PatchDrivingLessonStatus(drivingLessonId, patchRequest.Status.Value);
            return NoContent();
        }

        [HttpDelete("driving-lessons/{drivingLessonId}")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IActionResult> DeleteDrivingLesson([FromRoute][Required] int drivingLessonId)
        {
            await _drivingLessonService.DeleteDrivingLessonAsync(drivingLessonId);
            return NoContent();
        }
    }
}

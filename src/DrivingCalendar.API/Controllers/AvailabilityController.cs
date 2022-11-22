using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DrivingCalendar.API.Controllers
{

    [Route("api")]
    [ApiController]
    [Authorize]
    public class AvailabilityController : ControllerBase
    {

        private readonly IAvailabilityService _availabilityService;
        private readonly IContextService _contextService;

        public AvailabilityController(IAvailabilityService availabilityService, IContextService contexrService)
        {
            _availabilityService = availabilityService;
            _contextService = contexrService;
        }

        [HttpPost("users/{userId}/availabilities")]
        public async Task<IActionResult> Create([FromBody][Required] CreateAvailabilityRequest createAvailabilityRequest, [FromRoute][Required] int userId)
        {
            CreateAvailability createAvailability = new()
            {
                UserId = userId,
                StartDate = createAvailabilityRequest.StartDate,
                EndDate = createAvailabilityRequest.EndDate,
            };

            IList<int> ids = await _availabilityService.CreateAvailabilityAsync(createAvailability, createAvailabilityRequest.Repeat);

            return new ObjectResult(ids) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("users/{userId}/availabilities")]
        public async Task<IList<Availability>> GetAvailabilities([FromRoute][Required] int userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            return await _availabilityService.GetAvailabilities(userId, startDate, endDate);
        }
    }
}

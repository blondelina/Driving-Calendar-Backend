using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DrivingCalendar.API.Host
{

    [Route("api")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {

        private readonly IAvailabityService _availabityService;

        public AvailabilityController(IAvailabityService availabityService)
        {
            _availabityService= availabityService ;
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

            IList<int> ids = await _availabityService.CreateAvailabilityAsync(createAvailability, createAvailabilityRequest.Repeat);

            return new ObjectResult(ids) { StatusCode = StatusCodes.Status201Created };
        }
    }
}

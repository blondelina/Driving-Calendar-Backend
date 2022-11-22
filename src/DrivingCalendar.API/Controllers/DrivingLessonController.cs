using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrivingCalendar.API.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{drivingLessonId}")]

        public async Task<ActionResult<DrivingLesson>> GetById([FromRoute] int drivingLessonId)
        {
            DrivingLesson drivingLesson = await _drivingLessonService.GetByIdAsync(drivingLessonId);
            return drivingLesson is not null ? drivingLesson : NotFound();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrivingCalendar.Business.Abstractions.Services;
using DrivingCalendar.Business.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrivingCalendar.API.Host
{
    [Route("api/[controller]")]
    [ApiController]
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

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> AddDrivingLesson()
        {
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

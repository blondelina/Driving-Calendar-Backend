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
    public class StudnetController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudnetController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("/students")]
        [Authorize(Roles = IdentityRoles.INSTRUCTOR)]
        public async Task<IList<DisplayedStudent>> GetStudents()
        {
            IList<Student> students = await _studentService.GetStudents();
            IList<DisplayedStudent> response = students.Select(s => new DisplayedStudent
            {
                studentId = s.Id,
                studentUserName = s.UserName
            }).ToList();

            return response;
        }


    }
}

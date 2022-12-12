using DrivingCalendar.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IStudentInstructorService
    {
        Task<IList<Student>> GetListStudentsOfAnInstructor();
    }
}

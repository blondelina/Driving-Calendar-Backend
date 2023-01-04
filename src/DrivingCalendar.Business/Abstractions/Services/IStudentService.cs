using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IStudentService
    {
        Task<IList<Student>> GetStudents();

        Task<IList<Student>> GetStudentsNotAssignedToInstructors(IList<int> instructorIds);
    }
}

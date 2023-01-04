using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IStudentsRepository
    {
        Task<IList<Instructor>> GetStudentInstructors(int studentId);
        Task<IList<Student>> GetStudents();

        Task<IList<Student>> GetStudentsNotAssignedToInstructor(IList<int> instructorIds);
    }
}

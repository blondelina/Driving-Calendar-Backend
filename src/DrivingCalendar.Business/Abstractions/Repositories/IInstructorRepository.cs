using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IInstructorRepository
    {
        Task<IList<Student>> GetInstructorStudents(int instructorId);

        Task<int> AddStudent(int studentId, int instructorId);
        Task<bool> DeleteStudent(int studentId, int instructorId);

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.Business.Abstractions.Repositories
{
    public interface IStudentsRepository
    {
        Task<IList<Instructor>> GetStudentInstructors(int studentId);
    }
}

using DrivingCalendar.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.Business.Abstractions.Services
{
    public interface IInstructorService 
    {
        Task<int> AddStudentToInstructor(int studentId, int instructorId);
        Task<IList<Student>> GetStudents(int instructorId);
        Task<bool> DeletetStudentsFromInstrutor(int studentId, int instructorId);

    }
}

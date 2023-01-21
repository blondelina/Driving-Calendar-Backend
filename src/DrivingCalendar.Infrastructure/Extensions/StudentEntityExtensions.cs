using DrivingCalendar.Business.Models;
using DrivingCalendar.Business.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DrivingCalendar.Infrastructure.Extensions
{
    internal static class StudentEntityExtensions
    {
        public static IQueryable<Student> ApplyFiltering(this IQueryable<Student> query, StudentsFilter filter, ApplicationDbContext context)
        {
            if (filter.InstructorIds is not null && filter.InstructorIds.Any())
            {
                query = query.Where(s => context.StudentInstructors
                                                         .Any(si => filter.InstructorIds.Contains(si.InstructorId) && si.StudentId == s.Id));
            }

            if (filter.NotAssignedToInstructorIds is not null && filter.NotAssignedToInstructorIds.Any())
            {
                query = query.Where(s => !context.StudentInstructors
                                                         .Any(si => filter.NotAssignedToInstructorIds.Contains(si.InstructorId) && si.StudentId == s.Id));
            }

            if (filter.SearchString is not null)
            {
                string searchString = filter.SearchString.ToLower();
                query = query.Where(s => s.FirstName.ToLower().StartsWith(searchString.ToLower()) ||
                                         s.LastName.ToLower().StartsWith(searchString.ToLower()) ||
                                         s.Email.ToLower().StartsWith(searchString.ToLower()) ||
                                         s.UserName.ToLower().StartsWith(searchString.ToLower()));
            }

            return query;
        }
    }
}

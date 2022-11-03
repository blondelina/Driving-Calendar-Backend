using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingCalendar.Infrastructure
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DrivingInstructor> DrivingInstructors { get; set; }
    }
}

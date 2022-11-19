using DrivingCalendar.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingCalendar.Infrastructure
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<DrivingInstructorEntity> DrivingInstructors { get; set; }
        public DbSet<BaseUserEntity> BaseUsers { get; set; }
        public DbSet<DrivingLessonEntity> DrivingLessons { get; set; }
        public DbSet<AvailabilityEntity> Availabilities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BaseUserEntity>(builder =>
            {
                builder.HasIndex(e => e.Username)
                    .IsUnique();
            });
        }
    }
}

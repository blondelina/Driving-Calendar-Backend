using InfrastructureAPP;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace APIs.Controllers
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
             : base(options)
        {

        }
        public DbSet<DrivingInstructor>drivingInstructorSet{ get; set; }

    }
}

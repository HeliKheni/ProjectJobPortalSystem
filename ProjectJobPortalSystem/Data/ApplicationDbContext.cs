using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Models;

namespace ProjectJobPortalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployerModel> Employers { get; set; }

        public DbSet<JobsModel> Jobs { get; set; }

        public DbSet<JobSeekerModel> JobSeekers { get; set; }
    }
}
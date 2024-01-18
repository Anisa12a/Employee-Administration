using EmployeeAdministration.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdministration.DbContexts
{
    public class AdministrationContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }

        public AdministrationContext(DbContextOptions<AdministrationContext> options) : base(options)
        {
        }
    }
}

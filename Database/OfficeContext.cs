using Microsoft.EntityFrameworkCore;

namespace Employees.Database
{
    public class OfficeContext : DbContext
    {
        public OfficeContext(DbContextOptions options) : base(options)
        {
        }

        public OfficeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=appdb;Trusted_Connection=True;");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    }
}

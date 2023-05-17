using BlazorFullStackCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrud.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "SE" },
                new Department { Id = 2, Name = "CS" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Usama",
                    LastName = "Bajwa",
                    Course = "Web",
                    DepartmentId = 1,
                },
                new Student
                {
                    Id = 2,
                    FirstName = "haider",
                    LastName = "ali",
                    Course = "Data",
                    DepartmentId = 2
                }
            );
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Department> Department { get; set; }

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

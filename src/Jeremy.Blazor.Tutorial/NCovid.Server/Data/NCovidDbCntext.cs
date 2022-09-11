using Microsoft.EntityFrameworkCore;
using NCovid.Server.Entities;
using NCovid.Shared.Dtos;

namespace NCovid.Server.Data
{
    public class NCovidDbCntext : DbContext
    {
        public NCovidDbCntext(DbContextOptions<NCovidDbCntext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyHealth>().HasKey(x => new
            {
                x.EmployeeId,
                x.Date
            });

            modelBuilder.Entity<Employee>().HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailyHealth>().HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "R&D" },
                new Department { Id = 2, Name = "Market" },
                new Department { Id = 3, Name = "IT" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    DepartmentId = 1,
                    Name = "Katherine",
                    Gender = Gender.Female,
                    No = "001",
                    BirthDate = new DateTime(1992, 10, 10),
                    PictureUrl = ""
                },
                new Employee
                {
                    Id = 2,
                    DepartmentId = 1,
                    Name = "Demon",
                    Gender = Gender.Male,
                    No = "002",
                    BirthDate = new DateTime(1993, 8, 10),
                    PictureUrl = ""
                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 2,
                    Name = "Tina",
                    Gender = Gender.Female,
                    No = "003",
                    BirthDate = new DateTime(1999, 10, 15),
                    PictureUrl = ""
                },
                new Employee
                {
                    Id = 4,
                    DepartmentId = 2,
                    Name = "Amenda",
                    Gender = Gender.Female,
                    No = "004",
                    BirthDate = new DateTime(1987, 1, 12),
                    PictureUrl = ""
                },
                new Employee
                {
                    Id = 5,
                    DepartmentId = 3,
                    Name = "Talor",
                    Gender = Gender.Male,
                    No = "005",
                    BirthDate = new DateTime(1991, 8, 9),
                    PictureUrl = ""
                },
                new Employee
                {
                    Id = 6,
                    DepartmentId = 3,
                    Name = "Jerferry",
                    Gender = Gender.Male,
                    No = "006",
                    BirthDate = new DateTime(1998, 6, 7),
                    PictureUrl = ""
                }
                );
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<DailyHealth> DailyHealths { get; set; }

    }
}

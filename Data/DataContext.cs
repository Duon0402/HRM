using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmpSalary> EmpSalaries { get; set; }
        public DbSet<DepartmentPosition> DepartmentPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(d => d.Department)
            //    .WithMany(e => e.Employees)
            //    .HasForeignKey(d => d.DepartmentId);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(p => p.Position)
            //    .WithMany(e => e.Employees)
            //    .HasForeignKey(p => p.PositionId);

            modelBuilder.Entity<Employee>()
                .HasMany(s => s.EmpSalarys)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(dp => dp.DepartmentPosition)
                .WithOne(e => e.Employee)
                .HasForeignKey<Employee>(dp => dp.DepartPositId);

            modelBuilder.Entity<DepartmentPosition>()
                .HasOne(dp => dp.Position)
                .WithMany(p => p.DepartmentPositions)
                .HasForeignKey(dp => dp.PositionId);

            modelBuilder.Entity<DepartmentPosition>()
                .HasOne(dp => dp.Department)
                .WithMany(d => d.DepartmentPositions)
                .HasForeignKey(dp => dp.DepartmentId);

        }
    }
}

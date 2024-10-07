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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Position)
                .WithMany(e => e.Employees)
                .HasForeignKey(p => p.PositionId);
        }
    }
}

﻿using HRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRM.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Position> Positions { get; set; }
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
        }
    }
}

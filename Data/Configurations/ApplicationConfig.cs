using HRM.Data;
using HRM.Data.Base;
using HRM.Data.Services;
using HRM.Data.Services.Interfaces;
using HRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRM.Data.Configurations
{
    public static class ApplicationConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            // dependency injection
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IEmpSalaryService, EmpSalaryService>();
            services.AddScoped<IDepartPositService, DepartPositService>();

            // identity config
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            services.AddHttpContextAccessor();
            services. Configure<IdentityOptions>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequiredLength = 8;

                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 options.Lockout.MaxFailedAccessAttempts = 5;
             });

            // cookie config
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Home/Index";
                options.AccessDeniedPath = "/Home/Index";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}

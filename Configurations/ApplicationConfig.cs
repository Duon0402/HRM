using HRM.Data;
using HRM.Data.Base;
using HRM.Data.Services;
using HRM.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRM.Configurations
{
    public static class ApplicationConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IEmpSalaryService, EmpSalaryService>();
            services.AddScoped<IDepartPositService, DepartPositService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}

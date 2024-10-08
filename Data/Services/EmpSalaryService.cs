using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HRM.Data.Services
{
    public class EmpSalaryService : EntityBaseRepository<EmpSalary>, IEmpSalaryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmpSalaryService(DataContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ValidateDate(EmpSalary empSalary)
        {
            if (empSalary.StartDate > empSalary.EndDate)
            {
                return false;
            }

            var overlappingTimeRange = await _context.EmpSalaries
                .Where(r => r.EmployeeId == empSalary.EmployeeId &&
                            empSalary.StartDate < r.EndDate && empSalary.EndDate > r.StartDate &&
                            r.Id != empSalary.Id)
                .ToListAsync();

            if (overlappingTimeRange.Any())
            {
                return false;
            }

            return true;
        }
    }
}
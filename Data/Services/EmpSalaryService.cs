using HRM.Data.Base;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HRM.Data.Services
{
    public class EmpSalaryService : EntityBaseRepository<EmpSalary>, IEmpSalaryService
    {
        private readonly DataContext _context;

        public EmpSalaryService(DataContext context) : base(context)
        {
            _context = context;
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
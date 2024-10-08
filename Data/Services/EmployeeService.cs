using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

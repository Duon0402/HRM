using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(DataContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
    }
}

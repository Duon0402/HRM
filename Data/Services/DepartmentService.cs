using HRM.Data.Base;
using HRM.Models;

namespace HRM.Data.Services
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

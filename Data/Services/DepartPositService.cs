using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class DepartPositService : EntityBaseRepository<DepartmentPosition>, IDepartPositService
    {
        private readonly DataContext _context;

        public DepartPositService(DataContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
    }
}

using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class PositionService : EntityBaseRepository<Position>, IPositionService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PositionService(DataContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

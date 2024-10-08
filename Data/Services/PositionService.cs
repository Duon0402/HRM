using HRM.Data.Base;
using HRM.Data.Services.Interfaces;
using HRM.Models;

namespace HRM.Data.Services
{
    public class PositionService : EntityBaseRepository<Position>, IPositionService
    {
        private readonly DataContext _context;

        public PositionService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

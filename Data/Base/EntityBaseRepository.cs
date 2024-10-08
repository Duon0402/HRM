
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace HRM.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EntityBaseRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddAsync(T entity)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            entity.CreateBy = currentUser;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckExits(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).AnyAsync(predicate);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if(entity != null)
            {
                entity.IsDeleted = true;
                entity.DeleteAt = DateTime.Now;
            }
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query.Where(x => !x.IsDeleted);
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            entity.CreateBy = currentUser;
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

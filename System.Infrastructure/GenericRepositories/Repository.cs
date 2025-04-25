using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Application.Abstraction;
using System.Infrastructure.Persistence;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Shared.BaseModel;

namespace System.Infrastructure.GenericRepositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _httpContextAccessor = httpContextAccessor;
        }
        private string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            if (entity is IEntity baseEntity)
            {
                baseEntity.CreatedOn = DateTime.UtcNow;
                baseEntity.LastModifiedOn = DateTime.UtcNow;
                baseEntity.CreatedBy = GetCurrentUserId();
                baseEntity.LastModifiedBy = GetCurrentUserId();
            }
            await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            if (entity is IEntity baseEntity)
            {
                baseEntity.LastModifiedOn = DateTime.UtcNow;
                baseEntity.LastModifiedBy = GetCurrentUserId();
            }
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task SoftDeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null && entity is IEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedOn = DateTime.UtcNow;
                baseEntity.LastModifiedOn = DateTime.UtcNow;
                baseEntity.LastModifiedBy = GetCurrentUserId();
                _dbSet.Update(entity);
            }
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }
    }
}
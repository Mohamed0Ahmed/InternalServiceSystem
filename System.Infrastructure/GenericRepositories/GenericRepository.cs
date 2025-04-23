using Microsoft.EntityFrameworkCore;
using System.Application.Abstraction;
using System.Linq.Expressions;
using System.Shared.BaseModel;

namespace System.Infrastructure.GenericRepositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey>
                                    where T : BaseEntity<TKey>
                                    where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && entity.IsDeleted)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query
                .Where(e => !e.IsDeleted)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public async Task SoftDeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with ID {id} not found.");
            }
            Delete(entity);
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
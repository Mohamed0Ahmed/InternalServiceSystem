using System.Linq.Expressions;
using System.Shared.BaseModel;

namespace System.Application.Abstraction
{
    public interface IGenericRepository<T, TKey> where T : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SoftDeleteAsync(TKey id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
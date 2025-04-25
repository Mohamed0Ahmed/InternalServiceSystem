using System.Shared.BaseModel;

namespace System.Application.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, TKey> Repository<T, TKey>()
                             where T : BaseEntity<TKey>
                             where TKey : IEquatable<TKey>;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
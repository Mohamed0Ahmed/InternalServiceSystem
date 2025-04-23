using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Application.Abstraction;
using System.Infrastructure.GenericRepositories;
using System.Infrastructure.Persistence;
using System.Shared.BaseModel;

namespace System.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T, TKey> Repository<T, TKey>() where T : BaseEntity<TKey> where TKey : IEquatable<TKey>
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<T, TKey>(_context);
                _repositories[type] = repo;
            }

            return (IGenericRepository<T, TKey>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
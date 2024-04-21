using WebAppManager.Models;

namespace WebAppManager.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Public Methods

        public Task BeginTransactionAsync();

        public Task CommitAsync();

        public IGenericRepository<TUOWEntity>? GetRepository<TUOWEntity>() where TUOWEntity : BaseEntities;

        public Task RollbackAsync();

        public Task<int> SaveChangesAsync();

        #endregion Public Methods
    }
}

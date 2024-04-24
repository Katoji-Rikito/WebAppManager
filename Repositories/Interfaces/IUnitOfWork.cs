using WebAppManager.Models;

namespace WebAppManager.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Public Methods

        public Task BeginTransactionAsync();

        public Task CommitAsync();

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntities;

        public Task RollbackAsync();

        public Task<int> SaveChangesAsync();

        #endregion Public Methods
    }
}

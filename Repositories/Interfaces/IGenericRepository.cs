using System.Linq.Expressions;
using WebAppManager.Models;

namespace WebAppManager.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntities
    {
        #region Public Methods

        public Task CreateAsync(TEntity entityData);

        public Task CreateMultiAsync(List<TEntity> entityDatas);

        public Task DangerDeleteAsync(TEntity entityData);

        public Task DangerDeleteMultiAsync(List<TEntity> entityDatas);

        public Task DeleteAsync(TEntity entityData);

        public Task DeleteMultiAsync(List<TEntity> entityDatas);

        public Task<TEntity?> GetByIdAsync(string id);

        public Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null);

        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null);

        public Task UpdateAsync(TEntity entityData);

        public Task UpdateMultiAsync(List<TEntity> entityDatas);

        #endregion Public Methods
    }
}

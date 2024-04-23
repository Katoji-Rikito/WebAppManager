using System.Linq.Expressions;
using WebAppManager.Models;

namespace WebAppManager.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntities
    {
        #region Public Methods

        public Task<TEntity> CreateAsync(TEntity entityData);

        public Task<IEnumerable<TEntity>> CreateMultiAsync(List<TEntity> entityDatas);

        public Task<TEntity> DangerDeleteAsync(TEntity entityData);

        public Task<IEnumerable<TEntity>> DangerDeleteMultiAsync(List<TEntity> entityDatas);

        public Task<TEntity> DeleteAsync(TEntity entityData);

        public Task<IEnumerable<TEntity>> DeleteMultiAsync(List<TEntity> entityDatas);

        public Task<TEntity?> GetByIdAsync(string id);

        public Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null);

        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null);

        public Task<bool> IsRecordExists(Expression<Func<TEntity, bool>> filter);

        public Task<TEntity> UpdateAsync(TEntity entityData);

        public Task<IEnumerable<TEntity>> UpdateMultiAsync(List<TEntity> entityDatas);

        public Task<int> SaveChangesAsync();
        #endregion Public Methods
    }
}

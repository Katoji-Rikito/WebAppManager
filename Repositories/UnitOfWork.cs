using Microsoft.EntityFrameworkCore.Storage;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly WebappmanagerContext _context;
        private readonly Dictionary<Type, object> _repoDict = new Dictionary<Type, object>();
        private IDbContextTransaction? _transaction;
        private bool disposed = false;

        #endregion Private Fields

        public UnitOfWork(WebappmanagerContext context) { _context = context; }

        #region Public Methods

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// <returns> </returns>
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commit thay đổi
        /// </summary>
        /// <returns> </returns>
        /// <exception cref="Exception"> </exception>
        public async Task CommitAsync()
        {
            try
            {
                if (_transaction is not null)
                    await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                if (_transaction is not null)
                    await _transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        /// <summary>
        /// Ngắt kết nối đến CSDL
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<TEntity>? GetRepository<TEntity>() where TEntity : BaseEntities
        {
            if (_repoDict.ContainsKey(typeof(TEntity))) { return _repoDict[typeof(TEntity)] as IGenericRepository<TEntity>; }
            var repo = new GenericRepository<TEntity>(_context);
            _repoDict.Add(typeof(TEntity), repo);
            return repo;
        }

        /// <summary>
        /// Huỷ thay đổi dữ liệu
        /// </summary>
        /// <returns> </returns>
        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
            await _transaction!.DisposeAsync();
            _transaction = null!;
        }

        /// <summary>
        /// Lưu toàn bộ thay đổi dữ liệu
        /// </summary>
        /// <returns> Số dòng được lưu vào CSDL </returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Public Methods



        #region Protected Methods

        /// <summary>
        /// Huỷ kết nối CSDL (?)
        /// </summary>
        /// <param name="disposing"> Huỷ hay không (?) </param>
        /// <returns> </returns>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) _context.Dispose();
            this.disposed = true;
        }

        #endregion Protected Methods
    }
}

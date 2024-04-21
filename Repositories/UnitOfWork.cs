using Microsoft.EntityFrameworkCore.Storage;
using WebAppManager.Models;
using WebAppManager.Repositories.Interfaces;

namespace WebAppManager.Repositories
{
    public class UnitOfWork(WebappmanagerContext context) : IUnitOfWork
    {
        #region Private Fields

        private readonly WebappmanagerContext _context = context;
        private readonly Dictionary<Type, object> _repoDict = new Dictionary<Type, object>();
        private IDbContextTransaction? _transaction;
        private bool disposed = false;

        #endregion Private Fields



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
            try { await _transaction!.CommitAsync(); }
            catch (Exception ex)
            {
                await _transaction!.RollbackAsync();
                throw new Exception(ex.Message);
            }
            finally
            {
                await _transaction!.DisposeAsync();
                _transaction = null!;
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

        public IGenericRepository<TUOWEntity>? GetRepository<TUOWEntity>() where TUOWEntity : BaseEntities
        {
            if (_repoDict.ContainsKey(typeof(TUOWEntity))) { return _repoDict[typeof(TUOWEntity)] as IGenericRepository<TUOWEntity>; }
            var repo = new GenericRepository<TUOWEntity>(_context);
            _repoDict.Add(typeof(TUOWEntity), repo);
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

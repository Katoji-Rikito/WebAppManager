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

        private IDbContextTransaction _transaction = null!;

        private bool disposed = false;

        #endregion Private Fields
        #region Public Constructors

        #endregion Public Constructors



        #region Public Methods

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// <returns> </returns>
        public async Task BeginTransactionAsync()
        {
            try { _transaction = await _context.Database.BeginTransactionAsync(); }
            catch (Exception ex) { throw new Exception(ex.Message); }
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
                if (_transaction is null) throw new Exception(CommonMessages.TransactionIsNull);
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
                    _transaction = null!;
                }
            }
        }

        /// <summary>
        /// Ngắt kết nối đến CSDL
        /// </summary>
        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Lấy repository cần xử lý
        /// </summary>
        /// <typeparam name="TEntity"> Entity cần xử lý </typeparam>
        /// <returns> Repository của entity cần xử lý </returns>
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntities
        {
            try
            {
                if (_repoDict.ContainsKey(typeof(TEntity)))
                {
                    var repoDict = _repoDict[typeof(TEntity)] as IGenericRepository<TEntity>;
                    if (repoDict is not null)
                        return repoDict;
                }

                var repo = new GenericRepository<TEntity>(_context);
                _repoDict.Add(typeof(TEntity), repo);
                return repo;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Huỷ thay đổi dữ liệu
        /// </summary>
        /// <returns> </returns>
        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction is null) throw new Exception(CommonMessages.TransactionIsNull);
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { if (_transaction is not null) _transaction = null!; }
        }

        /// <summary>
        /// Lưu toàn bộ thay đổi dữ liệu
        /// </summary>
        /// <returns> Số dòng được lưu vào CSDL </returns>
        public async Task<int> SaveChangesAsync()
        {
            try { return await _context.SaveChangesAsync(); }
            catch (Exception ex) { throw new Exception(ex.Message); }
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
            try
            {
                if (!this.disposed && disposing) _context.Dispose();
                this.disposed = true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion Protected Methods
    }
}

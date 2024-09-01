using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAppManager.Models;

namespace WebAppManager.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// <returns> </returns>
        public Task BeginTransactionAsync();

        /// <summary>
        /// Chốt kết quả thay đổi dữ liệu vào CSDL
        /// </summary>
        /// <returns> </returns>
        public Task CommitAsync();

        /// <summary>
        /// Lấy repository cần xử lý
        /// </summary>
        /// <typeparam name="TEntity"> Entity cần xử lý </typeparam>
        /// <returns> Repository của entity cần xử lý </returns>
        public IGenericRepository<TEntity> GetRepository<TDbContext, TEntity>()
            where TDbContext : DbContext
            where TEntity : BaseEntities;

        /// <summary>
        /// Huỷ thay đổi dữ liệu
        /// </summary>
        /// <returns> </returns>
        public Task RollbackAsync();

        /// <summary>
        /// Lưu tạm thời thay đổi dữ liệu
        /// </summary>
        /// <returns> Số thay đổi vừa ghi nhận </returns>
        public Task<int> SaveChangesAsync();

        #endregion Public Methods
    }

    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        /// Lưu trữ các DbContext
        /// </summary>
        private readonly Dictionary<Type, DbContext> _dbContextDict = [];

        /// <summary>
        /// Lưu trữ các repository
        /// </summary>
        private readonly Dictionary<Type, object> _repoDict = [];

        /// <summary>
        /// Khai báo đẻ lấy DbContext đã được Dependency Injection
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Lưu trữ transaction
        /// </summary>
        private readonly Dictionary<DbContext, IDbContextTransaction> _transaction = [];

        /// <summary>
        /// Theo dõi trạng thái đã giải phóng tài nguyên hay chưa
        /// </summary>
        private bool disposedValue = false;

        #endregion Private Fields



        #region Private Destructors

        /// <summary>
        /// Nếu phương thức Dispose() không được gọi một cách thủ công, thì các tài nguyên không được quản lý vẫn sẽ được giải phóng khi đối tượng UnitOfWork bị hủy bởi bộ thu gom rác
        /// </summary>
        ~UnitOfWork()
        {
            // Không thay đổi mã này. Đặt mã dọn dẹp trong phương thức 'Dispose(bool disposing)'
            Dispose(disposing: false);
        }

        #endregion Private Destructors



        #region Private Methods

        /// <summary>
        /// Ngắt kết nối với CSDL
        /// </summary>
        /// <returns> </returns>
        private async Task DisposeAsync()
        {
            try
            {
                foreach (IDbContextTransaction transaction in _transaction.Values)
                {
                    await transaction.DisposeAsync();
                }
                _transaction.Clear();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Lấy DbContext cần xử lý
        /// </summary>
        /// <typeparam name="TDbContext"> DbContext cần xử lý </typeparam>
        /// <returns> DbContext cần xử lý </returns>
        private TDbContext GetDbContext<TDbContext>() where TDbContext : DbContext
        {
            try
            {
                if (_dbContextDict.ContainsKey(typeof(TDbContext)))
                {
                    TDbContext? dbContextFromDict = _dbContextDict[typeof(TDbContext)] as TDbContext;
                    if (dbContextFromDict is not null)
                    {
                        return dbContextFromDict;
                    }
                }

                TDbContext dbContext = _serviceProvider.GetRequiredService<TDbContext>();
                _dbContextDict.Add(typeof(TDbContext), dbContext);
                return dbContext;
            }
            catch (Exception) { throw; }
        }

        #endregion Private Methods

        /// <summary>
        /// Huỷ kết nối CSDL
        /// </summary>
        /// <param name="disposing"> Phương thức huỷ được gọi hay không? True là đươc gọi </param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                        foreach (DbContext dbContext in _dbContextDict.Values)
                        {
                            dbContext.Dispose();
                        }
                        _dbContextDict.Clear();
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }
            catch (Exception) { throw; }
        }

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task BeginTransactionAsync()
        {
            try
            {
                foreach (DbContext dbContext in _dbContextDict.Values)
                {
                    if (dbContext.Database.CurrentTransaction is null)
                    {
                        _transaction[dbContext] = await dbContext.Database.BeginTransactionAsync();
                    }
                }
            }
            catch (Exception) { throw; }
        }

        public async Task CommitAsync()
        {
            try
            {
                foreach (IDbContextTransaction transaction in _transaction.Values)
                {
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
            finally { await DisposeAsync(); }
        }

        /// <summary>
        /// Ngắt kết nối đến CSDL
        /// </summary>
        public void Dispose()
        {
            try
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
            catch (Exception) { throw; }
        }

        public IGenericRepository<TEntity> GetRepository<TDbContext, TEntity>()
            where TDbContext : DbContext
            where TEntity : BaseEntities
        {
            try
            {
                if (_repoDict.ContainsKey(typeof(TEntity)))
                {
                    IGenericRepository<TEntity>? repoDict = _repoDict[typeof(TEntity)] as IGenericRepository<TEntity>;
                    if (repoDict is not null)
                    {
                        return repoDict;
                    }
                }

                TDbContext dbContext = GetDbContext<TDbContext>();
                GenericRepository< TEntity> repo = new(dbContext);
                _repoDict.Add(typeof(TEntity), repo);
                return repo;
            }
            catch (Exception) { throw; }
        }

        public async Task RollbackAsync()
        {
            try
            {
                foreach (IDbContextTransaction transaction in _transaction.Values)
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception) { throw; }
            finally { await DisposeAsync(); }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                int totalChanges = 0;
                foreach (DbContext dbContext in _dbContextDict.Values)
                {
                    totalChanges += await dbContext.SaveChangesAsync();
                }
                return totalChanges;
            }
            catch (Exception) { throw; }
        }
    }
}
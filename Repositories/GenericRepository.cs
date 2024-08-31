using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppManager.Models;

namespace WebAppManager.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntities
    {
        #region Public Methods

        /// <summary>
        /// Tạo 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần tạo </param>
        /// <returns> Dữ liệu vừa tạo </returns>
        public Task<TEntity> CreateAsync(TEntity? entityData = null);

        /// <summary>
        /// Tạo nhiều dòng dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần tạo </param>
        /// <returns> Danh sách dữ liệu vừa tạo </returns>
        public Task<IEnumerable<TEntity>> CreateMultiAsync(List<TEntity>? entityDatas = null);

        /// <summary>
        /// Xoá bỏ hẳn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần xóa </param>
        /// <returns> </returns>
        public Task DangerDeleteAsync(TEntity? entityData = null);

        /// <summary>
        /// Xoá bỏ hẳn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần xóa </param>
        /// <returns> </returns>
        public Task DangerDeleteMultiAsync(List<TEntity>? entityDatas = null);

        /// <summary>
        /// Ẩn dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần ẩn </param>
        /// <returns> Dữ liệu vừa ẩn </returns>
        public Task<TEntity> DeleteAsync(TEntity? entityData = null);

        /// <summary>
        /// Ẩn nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần ẩn </param>
        /// <returns> Danh sách dữ liệu vừa ẩn </returns>
        public Task<IEnumerable<TEntity>> DeleteMultiAsync(List<TEntity>? entityDatas = null);

        /// <summary>
        /// Lấy 1 dòng dữ liệu theo điều kiện (Nếu có)
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy (Nếu có) </param>
        /// <returns> Dữ liệu của điều kiện vừa truyền vào </returns>
        public Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Lấy 1 dòng dữ liệu theo ID
        /// </summary>
        /// <param name="id"> ID dòng cần lấy </param>
        /// <returns> Dữ liệu của ID vừa truyền vào </returns>
        public Task<TEntity?> GetDataByIdAsync(int id = -1);

        /// <summary>
        /// Lấy 1 dòng dữ liệu theo câu truy vấn
        /// </summary>
        /// <param name="query"> Câu query lấy dữ liệu </param>
        /// <returns> Dữ liệu thoả mãn truy vấn vừa truyền </returns>
        public Task<TEntity?> GetDataByQueryAsync(FormattableString? query = null);

        /// <summary>
        /// Lấy nhiều dòng dữ liệu theo điều kiện truyền vào. Nếu không có điều kiện truyền vào thì lấy toàn bộ bảng
        /// </summary>
        /// <param name="filter"> Điều kiện cần lấy (Nếu có) </param>
        /// <returns> Danh sách dữ liệu theo điều kiện vừa truyền vào </returns>
        public Task<IEnumerable<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Lấy danh sách dữ liệu theo câu truy vấn
        /// </summary>
        /// <param name="query"> Câu query lấy dữ liệu </param>
        /// <returns> Dữ liệu thoả mãn truy vấn vừa truyền </returns>
        public Task<IEnumerable<TEntity>?> GetListByQueryAsync(FormattableString? query = null);

        /// <summary>
        /// Kiểm tra xem đã tồn tại bản ghi hay chưa
        /// </summary>
        /// <param name="filter"> Điều kiện cần kiểm tra </param>
        /// <returns> True nếu đã tồn tại </returns>
        public Task<bool> IsRecordExists(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Cập nhật 1 dòng dữ liệu
        /// </summary>
        /// <param name="entityData"> Dữ liệu cần cập nhật </param>
        /// <returns> Dữ liệu vừa cập nhật </returns>
        public Task<TEntity> UpdateAsync(TEntity? entityData = null);

        /// <summary>
        /// Cập nhật nhiều dữ liệu
        /// </summary>
        /// <param name="entityDatas"> Danh sách dữ liệu cần cập nhật </param>
        /// <returns> Danh sách dữ liệu vừa cập nhật </returns>
        public Task<IEnumerable<TEntity>> UpdateMultiAsync(List<TEntity>? entityDatas = null);

        #endregion Public Methods
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
            where TEntity : BaseEntities
    {
        #region Private Fields

        private readonly DbContext _context;

        private readonly DbSet<TEntity> _dbSetEntity;

        #endregion Private Fields

        #region Public Constructors

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSetEntity = _context.Set<TEntity>();
        }

        #endregion Public Constructors



        #region Public Methods

        public async Task<TEntity> CreateAsync(TEntity? entityData = null)
        {
            try
            {
                if (entityData is null)
                {
                    throw new ArgumentNullException(nameof(entityData));
                }

                entityData.IsAble = true;
                entityData.UpdatedAt = DateTime.Now;
                _ = await _dbSetEntity.AddAsync(entityData);
                _ = await _context.SaveChangesAsync();
                return entityData;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>> CreateMultiAsync(List<TEntity>? entityDatas = null)
        {
            try
            {
                if (entityDatas is null)
                {
                    throw new ArgumentNullException(nameof(entityDatas));
                }

                entityDatas.ForEach(record =>
                {
                    record.IsAble = true;
                    record.UpdatedAt = DateTime.Now;
                });
                await _dbSetEntity.AddRangeAsync(entityDatas);
                _ = await _context.SaveChangesAsync();
                return entityDatas;
            }
            catch (Exception) { throw; }
        }

        public async Task DangerDeleteAsync(TEntity? entityData = null)
        {
            try
            {
                if (entityData is null)
                {
                    throw new ArgumentNullException(nameof(entityData));
                }

                _ = await Task.Run(() => _dbSetEntity.Remove(entityData));
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task DangerDeleteMultiAsync(List<TEntity>? entityDatas = null)
        {
            try
            {
                if (entityDatas is null)
                {
                    throw new ArgumentNullException(nameof(entityDatas));
                }

                await Task.Run(() => _dbSetEntity.RemoveRange(entityDatas));
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> DeleteAsync(TEntity? entityData = null)
        {
            try
            {
                if (entityData is null)
                {
                    throw new ArgumentNullException(nameof(entityData));
                }

                entityData.IsAble = false;
                entityData.UpdatedAt = DateTime.Now;
                _ = await Task.Run(() => _dbSetEntity.Update(entityData));
                _ = await _context.SaveChangesAsync();
                return entityData;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>> DeleteMultiAsync(List<TEntity>? entityDatas = null)
        {
            try
            {
                if (entityDatas is null)
                {
                    throw new ArgumentNullException(nameof(entityDatas));
                }

                entityDatas.ForEach(record =>
                {
                    record.IsAble = false;
                    record.UpdatedAt = DateTime.Now;
                });
                await Task.Run(() => _dbSetEntity.UpdateRange(entityDatas));
                _ = await _context.SaveChangesAsync();
                return entityDatas;
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity?> GetDataAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            try
            {
                return filter is null ? await _dbSetEntity.FirstOrDefaultAsync() : await _dbSetEntity.FirstOrDefaultAsync(filter);
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity?> GetDataByIdAsync(int id = -1)
        {
            try { return await _dbSetEntity.FindAsync(id); }
            catch (Exception) { throw; }
        }

        public async Task<TEntity?> GetDataByQueryAsync(FormattableString? query = null)
        {
            try
            {
                return query is not null ? await _dbSetEntity.FromSql(query).FirstOrDefaultAsync() : throw new ArgumentNullException(nameof(query));
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            try
            {
                return filter is not null ? await _dbSetEntity.Where(filter).ToListAsync() : await _dbSetEntity.ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>?> GetListByQueryAsync(FormattableString? query = null)
        {
            try
            {
                return query is not null ? await _dbSetEntity.FromSql(query).ToListAsync() : throw new ArgumentNullException(nameof(query));
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> IsRecordExists(Expression<Func<TEntity, bool>>? filter = null)
        {
            try
            {
                return filter is not null ? await _dbSetEntity.AnyAsync(filter) : throw new ArgumentNullException(nameof(filter));
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> UpdateAsync(TEntity? entityData = null)
        {
            try
            {
                if (entityData is null)
                {
                    throw new ArgumentNullException(nameof(entityData));
                }

                entityData.UpdatedAt = DateTime.Now;
                _ = await Task.Run(() => _dbSetEntity.Update(entityData));
                _ = await _context.SaveChangesAsync();
                return entityData;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>> UpdateMultiAsync(List<TEntity>? entityDatas = null)
        {
            try
            {
                if (entityDatas is null)
                {
                    throw new ArgumentNullException(nameof(entityDatas));
                }

                entityDatas.ForEach(record => record.UpdatedAt = DateTime.Now);
                await Task.Run(() => _dbSetEntity.UpdateRange(entityDatas));
                _ = await _context.SaveChangesAsync();
                return entityDatas;
            }
            catch (Exception) { throw; }
        }

        #endregion Public Methods
    }
}